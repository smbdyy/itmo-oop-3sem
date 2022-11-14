using Backups.Exceptions;
using Zio;

namespace Backups.Models;

public class Storage
{
    private readonly List<BackupObject> _backupObjects;

    public Storage(string name, IEnumerable<BackupObject> backupObjects)
    {
        if (name == string.Empty || name.Contains('/') || name.Contains('\\'))
        {
            throw PathException.IncorrectStorageName(name);
        }

        Name = name;
        _backupObjects = new List<BackupObject>(backupObjects);
    }

    public IReadOnlyList<BackupObject> BackupObjects => _backupObjects;
    public UPath Name { get; }
}