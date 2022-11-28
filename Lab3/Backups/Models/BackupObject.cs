using Backups.Tools.Exceptions;

namespace Backups.Models;

public class BackupObject : IBackupObject
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