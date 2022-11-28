namespace Backups.Tools;

public class RepositoryException : Exception
{
    public RepositoryException(string message)
        : base(message) { }

    public static RepositoryException IncorrectRelativePath(string path)
    {
        return new RepositoryException($"incorrect relative path {path}");
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