namespace Backups.Tools.Exceptions;

public class ArchiverException : Exception
{
    public ArchiverException(string message)
        : base(message) { }

    public static ArchiverException CompositeNotBuilt()
    {
        return new ArchiverException($"visitor has not built repository object's composite");
    }
}