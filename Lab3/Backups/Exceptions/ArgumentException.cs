using Backups.Models;

namespace Backups.Exceptions;

public class ArgumentException : Exception
{
    public ArgumentException(string message)
        : base(message) { }

    public static ArgumentException EmptyList()
    {
        return new ArgumentException("given list must not be empty");
    }

    public static ArgumentException BackupObjectIsNotAdded()
    {
        return new ArgumentException("trying to delete not added backup object");
    }

    public static ArgumentException BackupObjectIsAlreadyAdded(BackupObject backupObject)
    {
        return new ArgumentException($"backup object with path {backupObject.RelativePath.FullName} already added");
    }
}