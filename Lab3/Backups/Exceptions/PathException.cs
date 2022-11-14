namespace Backups.Exceptions;

public class PathException : Exception
{
    public PathException(string message)
        : base(message) { }

    public static PathException DoesNotExist(string path)
    {
        return new PathException($"file or directory {path} does not exist");
    }

    public static PathException DirectoryAndFileWithSamePath(string path)
    {
        return new PathException($"there are both file and directory with path {path}");
    }

    public static PathException IncorrectStorageName(string name)
    {
        return new PathException($"storage name must not be empty nor contain \"\\\" or \"/\" ({name} is given)");
    }
}