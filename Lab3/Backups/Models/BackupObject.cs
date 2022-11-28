using ArgumentException = Backups.Tools.ArgumentException;

namespace Backups.Models;

public class BackupObject
{
    public BackupObject(string path)
    {
        if (path == string.Empty)
        {
            throw ArgumentException.EmptyPathString();
        }

        Path = path;
    }

    public string Path { get; }
}