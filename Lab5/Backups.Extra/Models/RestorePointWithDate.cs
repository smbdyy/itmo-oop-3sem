using Backups.Models;
using Backups.StorageAlgorithms;

namespace Backups.Extra.Models;

public class RestorePointWithDate : IRestorePoint
{
    private List<IBackupObject> _backupObjects;

    public RestorePointWithDate(IEnumerable<IBackupObject> backupObjects, IStorage storage, DateTime creationDateTime)
    {
        _backupObjects = backupObjects.ToList();
        Storage = storage;
        CreationDateTime = creationDateTime;
    }

    public IReadOnlyCollection<IBackupObject> BackupObjects => _backupObjects;
    public DateTime CreationDateTime { get; }
    public IStorage Storage { get; }
}