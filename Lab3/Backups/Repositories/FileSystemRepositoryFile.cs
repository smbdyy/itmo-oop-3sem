namespace Backups.Repositories;

public class FileSystemRepositoryFile : RepositoryFile
{
    private string _fullPath;

    public FileSystemRepositoryFile(string fullPath)
    {
        _fullPath = fullPath;
    }

    public override Stream Open()
    {
        // TODO file mode and access
        return File.Open(_fullPath, FileMode.Open, FileAccess.ReadWrite);
    }
}