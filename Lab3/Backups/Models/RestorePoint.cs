using Backups.StorageAlgorithms;

namespace Backups.Models;

public class RestorePoint
{
    private List<BackupObject> _backupObjects;

    public RestorePoint(IEnumerable<BackupObject> backupObjects, IStorage storage)
    {
        _backupObjects = backupObjects.ToList();
        Storage = storage;
    }

    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;
    public DateTime CreationDateTime { get; } = DateTime.Now;
    public IStorage Storage { get; }
}