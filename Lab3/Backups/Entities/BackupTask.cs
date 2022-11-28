using Backups.Archivers;
using Backups.Models;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using Backups.Tools;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<RestorePoint> _restorePoints = new ();
    private readonly List<BackupObject> _backupObjects = new ();
    private readonly IStorageAlgorithm _storageAlgorithm;
    private readonly IStorageArchiver _archiver;
    private int _newRestorePointId;

    public BackupTask(string name, IRepository repository, IStorageAlgorithm storageAlgorithm, IStorageArchiver archiver)
    {
        Name = repository.ValidateRelativePath(name);
        Repository = repository;
        _storageAlgorithm = storageAlgorithm;
        _archiver = archiver;
    }

    public string Name { get; }
    public IRepository Repository { get; }
    public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints;
    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;

    public void CreateRestorePoint()
    {
        IStorage storage = _storageAlgorithm.MakeStorage(GetNewRestorePointId(), Repository, _archiver, BackupObjects);
        _restorePoints.Add(new RestorePoint(BackupObjects, storage));
    }

    public void AddBackupObject(BackupObject backupObject)
    {
        if (_backupObjects.Any(obj => obj.Path == backupObject.Path))
        {
            throw BackupTaskException.AlreadyTracking(backupObject, this);
        }

        if (!Repository.DirectoryExists(backupObject.Path) && !Repository.FileExists(backupObject.Path))
        {
            throw BackupTaskException.NotFoundInRepository(backupObject, this);
        }

        _backupObjects.Add(backupObject);
    }

    public void RemoveBackupObject(BackupObject backupObject)
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