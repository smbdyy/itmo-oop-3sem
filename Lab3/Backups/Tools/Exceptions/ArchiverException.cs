namespace Backups.Tools.Exceptions;

public class ArchiverException : Exception
{
    public ArchiverException(string message)
        : base(message) { }

    public static ArchiverException CompositeNotBuilt()
    {
        return new ArchiverException("visitor has not built repository object's composite");
    }

    public static ArchiverException ArchiveObjectIsNotFile()
    {
        return new ArchiverException("archive repository object must be a file");
    }
}