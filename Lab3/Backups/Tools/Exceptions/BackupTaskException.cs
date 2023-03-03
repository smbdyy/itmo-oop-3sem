using Backups.Models;
using Backups.Services;

namespace Backups.Tools.Exceptions;

public class BackupTaskException : Exception
{
    public BackupTaskException(string message)
        : base(message) { }

    public static BackupTaskException AlreadyTracking(IBackupObject backupObject, IBackupTask backupTask)
    {
        return new BackupTaskException(
            $"backup object {backupObject.Path} is already tracking in backup task {backupTask.Name}");
    }

    public static BackupTaskException NotTracking(IBackupObject backupObject, IBackupTask backupTask)
    {
        return new BackupTaskException(
            $"backup object {backupObject.Path} isn't tracking in backup task {backupTask.Name}");
    }

    public static BackupTaskException NotFoundInRepository(IBackupObject backupObject, IBackupTask backupTask)
    {
        return new BackupTaskException(
            $"backup object {backupObject.Path} is not found in backup task {backupTask.Name} repository");
    }
}