namespace Backups.Tools;

public class ArgumentException : Exception
{
    public ArgumentException(string message)
        : base(message) { }

    public static ArgumentException EmptyPathString()
    {
        return new ArgumentException("path string can not be empty");
    }
}