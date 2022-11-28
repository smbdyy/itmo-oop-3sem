using Backups.StorageAlgorithms;

namespace Backups.Models;

public class RestorePoint : IRestorePoint
{
    private List<IBackupObject> _backupObjects;

    public RestorePoint(IEnumerable<IBackupObject> backupObjects, IStorage storage)
    {
        _backupObjects = backupObjects.ToList();
        Storage = storage;
    }

    public IReadOnlyCollection<IBackupObject> BackupObjects => _backupObjects;
    public DateTime CreationDateTime { get; } = DateTime.Now;
    public IStorage Storage { get; }
}