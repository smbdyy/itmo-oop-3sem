using Backups.Tools;

namespace Backups.Models;

public class BackupObject
{
    public BackupObject(string path)
    {
        if (path == string.Empty)
        {
            throw BackupsArgumentException.EmptyPathString();
        }

        Path = path;
    }

    public string Path { get; }
}