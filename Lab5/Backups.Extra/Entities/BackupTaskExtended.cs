using Backups.Archivers;
using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Models;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using Backups.Tools.Creators;

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
    private int _newRestorePointId;

    public BackupTaskExtended(
        string name,
        IRepository repository,
        IRestorePointDeleteSelector deleteSelector,
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
    }

    public string Name { get; }
    public string ArchiveExtension => _archiver.ArchiveExtension;
    public IRepository Repository { get; }
    public IReadOnlyCollection<IRestorePoint> RestorePoints => _restorePoints;
    public IReadOnlyCollection<IBackupObject> BackupObjects => _backupObjects;

    public void AddBackupObject(IBackupObject backupObject) { }
    public void RemoveBackupObject(IBackupObject backupObject) { }
    public void CreateRestorePoint() { }

    private void DeleteRestorePoint(IRestorePoint restorePoint)
    {
        if (!_restorePoints.Contains(restorePoint))
        {
            throw new NotImplementedException();
        }

        Repository.DeleteDirectory(Path.Combine(Repository.RestorePointsPath, restorePoint.FolderName));
        _restorePoints.Remove(restorePoint);
    }

    private int GetNewRestorePointId() => _newRestorePointId++;
}