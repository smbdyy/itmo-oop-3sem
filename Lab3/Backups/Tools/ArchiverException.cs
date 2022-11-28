using Backups.Repositories;

namespace Backups.Tools;

public class ArchiverException : Exception
{
    public ArchiverException(string message)
        : base(message) { }

    public static ArgumentException CompositeNotBuilt()
    {
        return new ArgumentException($"visitor has not built repository object' composite");
    }
}