using Backups.Archivers;
using Backups.Models;
using Backups.Repositories;
using Backups.StorageAlgorithms;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<RestorePoint> _restorePoints = new ();
    private readonly List<BackupObject> _backupObjects = new ();
    private readonly IStorageAlgorithm _storageAlgorithm;
    private readonly IStorageArchiver _archiver;
    private int _newRestorePointId = 0;

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

    private int GetNewRestorePointId()
    {
        _newRestorePointId++;
        return _newRestorePointId;
    }
}