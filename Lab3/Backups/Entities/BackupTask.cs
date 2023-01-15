using Backups.Archivers;
using Backups.Models;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using Backups.Tools.Creators;
using Backups.Tools.Exceptions;

namespace Backups.Entities;

public class BackupTask : IBackupTask
{
    private readonly List<IRestorePoint> _restorePoints = new ();
    private readonly List<IBackupObject> _backupObjects = new ();
    private readonly IStorageAlgorithm _storageAlgorithm;
    private readonly IStorageArchiver _archiver;
    private readonly IRestorePointCreator _restorePointCreator;
    private int _newRestorePointId;

    public BackupTask(
        string name,
        IRepository repository,
        IStorageAlgorithm storageAlgorithm,
        IStorageArchiver archiver,
        IRestorePointCreator restorePointCreator)
    {
        Name = repository.ValidateRelativePath(name);
        Repository = repository;
        _storageAlgorithm = storageAlgorithm;
        _archiver = archiver;
        _restorePointCreator = restorePointCreator;
    }

    public string Name { get; }
    public string ArchiveExtension => _archiver.ArchiveExtension;
    public IRepository Repository { get; }
    public IReadOnlyCollection<IRestorePoint> RestorePoints => _restorePoints;
    public IReadOnlyCollection<IBackupObject> BackupObjects => _backupObjects;

    public void CreateRestorePoint()
    {
        IEnumerable<IRepositoryObject> repositoryObjects =
            _backupObjects.Select(b => Repository.GetRepositoryObject(b.Path));

        int id = GetNewRestorePointId();
        string folderName = $"RestorePoint_{id}";
        string path = Path.Combine(Repository.RestorePointsPath, folderName);
        Repository.CreateDirectory(path);

        IStorage storage = _storageAlgorithm.MakeStorage(id, path, Repository, _archiver, repositoryObjects);
        _restorePoints.Add(_restorePointCreator.Create(folderName, _backupObjects, storage));
    }

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
    }

    public void RemoveBackupObject(IBackupObject backupObject)
    {
        if (!_backupObjects.Contains(backupObject))
        {
            throw BackupTaskException.NotTracking(backupObject, this);
        }

        _backupObjects.Remove(backupObject);
    }

    private int GetNewRestorePointId()
    {
        _newRestorePointId++;
        return _newRestorePointId;
    }
}