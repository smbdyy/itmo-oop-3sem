using Zio;

namespace Backups.Models;

public class Storage
{
    private readonly List<BackupObject> _backupObjects;

    public Storage(string name, IEnumerable<BackupObject> backupObjects)
    {
        if (name == string.Empty || name.Contains('/') || name.Contains('\\') || name.Contains(':'))
        {
            throw new NotImplementedException();
        }

        Name = name;
        _backupObjects = new List<BackupObject>(backupObjects);
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public string Name { get; }
}