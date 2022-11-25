namespace Backups.Models;

public class RestorePoint
{
    private List<BackupObject> _backupObjects;

    public RestorePoint(IEnumerable<BackupObject> backupObjects)
    {
        _backupObjects = backupObjects.ToList();
    }

    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;
    public DateTime CreationDateTime { get; } = DateTime.Now;
}