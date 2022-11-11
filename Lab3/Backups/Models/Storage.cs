namespace Backups.Models;

public class Storage
{
    private readonly List<BackupObject> _backupObjects;

    public Storage(IEnumerable<BackupObject> backupObjects)
    {
        _backupObjects = new List<BackupObject>(backupObjects);
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
}