using Backups.Archivers;
using Backups.Models;
using Backups.Repositories;
using Backups.StorageAlgorithms;

namespace Backups.Entities;

// TODO interface maybe
public class BackupTask
{
    private readonly List<RestorePoint> _restorePoints = new ();
    private readonly List<BackupObject> _backupObjects = new ();

    public BackupTask(string name, IRepository repository, IStorageAlgorithm storageAlgorithm, IStorageArchiver archiver)
    {
        Name = repository.ValidateRelativePath(name);
        Repository = repository;
        StorageAlgorithm = storageAlgorithm;
        Archiver = archiver;
    }

    public string Name { get; }
    public IRepository Repository { get; }
    public IStorageAlgorithm StorageAlgorithm { get; }
    public IStorageArchiver Archiver { get; }
    public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints;
    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;

    public void CreateRestorePoint()
    {
        IStorage storage = StorageAlgorithm.MakeStorage(Repository, Archiver, BackupObjects);
        _restorePoints.Add(new RestorePoint(BackupObjects, storage));
    }
}