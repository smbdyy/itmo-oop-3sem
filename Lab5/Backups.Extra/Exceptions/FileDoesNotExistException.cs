namespace Backups.Extra.Exceptions;

public class FileDoesNotExistException : Exception
{
    public FileDoesNotExistException(string filePath)
        : base($"file {filePath} does not exist") { }
}