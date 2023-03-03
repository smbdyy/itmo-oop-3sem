using Backups.Archivers;
using Backups.Extra.Interfaces;
using Backups.Extra.Loggers;
using Backups.Extra.Visitors;
using Backups.Models;
using Backups.Repositories;
using Backups.RestorePoints;
using Backups.RestorePoints.Creators;
using Backups.Services;
using Backups.StorageAlgorithms;
using Backups.Tools.Exceptions;

namespace Backups.Extra.Entities;

public class BackupTaskExtended : IBackupTask
{
    private readonly IRestorePointDeleteSelector _deleteSelector;
    private readonly IBackupTaskLogger _logger;
    private readonly List<IRestorePoint> _restorePoints = new ();
    private readonly List<IBackupObject> _backupObjects = new ();
    private readonly IStorageAlgorithm _storageAlgorithm;
    private readonly IStorageArchiver _archiver;
    private readonly IRestorePointCreator _restorePointCreator;
    private readonly IRestorePointsCleaner _cleaner;
    private int _newRestorePointId;

    public BackupTaskExtended(
        string name,
        IRepository repository,
        IRestorePointDeleteSelector deleteSelector,
        IRestorePointsCleaner cleaner,
        IBackupTaskLogger logger,
        IStorageAlgorithm storageAlgorithm,
        IStorageArchiver archiver,
        IRestorePointCreator restorePointCreator)
    {
        Name = repository.ValidateRelativePath(name);
        Repository = repository;
        _deleteSelector = deleteSelector;
        _logger = logger;
        _storageAlgorithm = storageAlgorithm;
        _archiver = archiver;
        _restorePointCreator = restorePointCreator;
        _cleaner = cleaner;
    }

    public string Name { get; }
    public string ArchiveExtension => _archiver.ArchiveExtension;
    public IRepository Repository { get; }
    public IReadOnlyCollection<IRestorePoint> RestorePoints => _restorePoints;
    public IReadOnlyCollection<IBackupObject> BackupObjects => _backupObjects;

    public void AddBackupObject(IBackupObject backupObject)
    {
        if (_backupObjects.Any(obj => obj.Path == backupObject.Path))
        {
            throw BackupTaskException.AlreadyTracking(backupObject, this);
        }

        Repository.ValidateRelativePath(backupObject.Path);
        if (!Repository.DirectoryExists(backupObject.Path) && !Repository.FileExists(backupObject.Path))
        {
            throw BackupTaskException.NotFoundInRepository(backupObject, this);
        }

        _backupObjects.Add(backupObject);
        _logger.WriteLog($"backup object added: {ObjectInfoMessageGenerator.GetInfo(backupObject)}");
    }

    public void RemoveBackupObject(IBackupObject backupObject)
    {
        if (!_backupObjects.Contains(backupObject))
        {
            throw BackupTaskException.NotTracking(backupObject, this);
        }

        _backupObjects.Remove(backupObject);
        _logger.WriteLog($"backup object removed: {ObjectInfoMessageGenerator.GetInfo(backupObject)}");
    }

    public void CreateRestorePoint()
    {
        IEnumerable<IRepositoryObject> repositoryObjects =
            _backupObjects.Select(b => Repository.GetRepositoryObject(b.Path));

        _restorePoints.Add(CreateRestorePointFromRepositoryObjects(repositoryObjects));
        _logger.WriteLog($"restore point created: {ObjectInfoMessageGenerator.GetInfo(_restorePoints.Last())}");
        CleanupRestorePoints();
    }

    public void RestoreToRepository(IRestorePoint restorePoint, IRepository repository)
    {
        if (!_restorePoints.Contains(restorePoint))
        {
            throw new BackupTaskException(
                $"restore point {restorePoint.FolderName} is not found in backup task {Name}");
        }

        var visitor = new CopyToRepositoryVisitor(repository);
        foreach (IRepositoryObject storageEntry in restorePoint.Storage.GetEntries())
        {
            storageEntry.Accept(visitor);
        }

        _logger.WriteLog($"restored: {ObjectInfoMessageGenerator.GetInfo(restorePoint)}");
    }

    private void DeleteRestorePoint(IRestorePoint restorePoint)
    {
        if (!_restorePoints.Contains(restorePoint))
        {
            throw new BackupTaskException(
                $"restore point {restorePoint.FolderName} is not found in backup task {Name}");
        }

        Repository.DeleteDirectory(Path.Combine(Repository.RestorePointsPath, restorePoint.FolderName));
        _restorePoints.Remove(restorePoint);
        _logger.WriteLog($"restore point deleted: {ObjectInfoMessageGenerator.GetInfo(restorePoint)}");
    }

    private void CleanupRestorePoints()
    {
        var pointsToDelete = _deleteSelector.GetRestorePointsToDelete(RestorePoints).ToList();
        if (pointsToDelete.Count == 0)
        {
            return;
        }

        if (_storageAlgorithm is SingleStorageAlgorithm || pointsToDelete.Count == RestorePoints.Count)
        {
            foreach (IRestorePoint point in pointsToDelete)
            {
                DeleteRestorePoint(point);
            }

            return;
        }

        IEnumerable<IRepositoryObject> newPointRepositoryObjects =
            _cleaner.GetNewPointRepositoryObjects(pointsToDelete, _restorePoints.Last());
        _restorePoints.Add(CreateRestorePointFromRepositoryObjects(newPointRepositoryObjects));
        foreach (IRestorePoint point in pointsToDelete)
        {
            DeleteRestorePoint(point);
        }
    }

    private IRestorePoint CreateRestorePointFromRepositoryObjects(
        IEnumerable<IRepositoryObject> repositoryObjects)
    {
        int id = GetNewRestorePointId();
        string folderName = $"RestorePoint_{id}";
        string path = Path.Combine(Repository.RestorePointsPath, folderName);
        Repository.CreateDirectory(path);

        var repositoryObjectsAsList = repositoryObjects.ToList();
        IStorage storage = _storageAlgorithm.MakeStorage(id, path, Repository, _archiver, repositoryObjectsAsList);
        IEnumerable<BackupObject> backupObjects = repositoryObjectsAsList.Select(o => new BackupObject(o.Path));
        return _restorePointCreator.Create(folderName, backupObjects, storage);
    }

    private int GetNewRestorePointId() => ++_newRestorePointId;
}