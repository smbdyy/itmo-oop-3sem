namespace Backups.Models;

public class BackupObject
{
    public BackupObject(string path)
    {
        if (path == string.Empty)
        {
            throw new NotImplementedException();
        }

        Path = path;
    }

    public string Path { get; }
}