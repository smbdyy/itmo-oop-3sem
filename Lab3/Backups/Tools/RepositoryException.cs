namespace Backups.Tools;

public class RepositoryException : Exception
{
    public RepositoryException(string message)
        : base(message) { }

    public static RepositoryException IncorrectRootPath(string path)
    {
        return new RepositoryException($"repository init failed: incorrect root path {path}");
    }

    public static RepositoryException FileNotFound(string path)
    {
        return new RepositoryException($"file {path} is not found in repository");
    }

    public static RepositoryException DirectoryNotFound(string path)
    {
        return new RepositoryException($"directory {path} is not found in repository");
    }
}