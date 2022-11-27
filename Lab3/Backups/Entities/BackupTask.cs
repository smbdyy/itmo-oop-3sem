using Backups.Archivers;
using Backups.Models;
using Backups.Repositories;
using Backups.StorageAlgorithms;

namespace Backups.Entities;

public class BackupTask
{
    private readonly List<RestorePoint> _restorePoints = new ();
    private readonly List<BackupObject> _backupObjects = new ();
    private IStorageAlgorithm _storageAlgorithm;
    private IStorageArchiver _archiver;

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
        IStorage storage = _storageAlgorithm.MakeStorage(Repository, _archiver, BackupObjects);
        _restorePoints.Add(new RestorePoint(BackupObjects, storage));
    }
}