namespace Backups.Tools.Exceptions;

public class BackupsArgumentException : Exception
{
    public BackupsArgumentException(string message)
        : base(message) { }

    public static BackupsArgumentException EmptyPathString()
    {
        return new BackupsArgumentException("path string can not be empty");
    }
}