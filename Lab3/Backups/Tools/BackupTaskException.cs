using Backups.Entities;
using Backups.Models;

namespace Backups.Tools;

public class BackupTaskException : Exception
{
    public BackupTaskException(string message)
        : base(message) { }

    public static BackupTaskException AlreadyTracking(BackupObject backupObject, BackupTask backupTask)
    {
        return new BackupTaskException(
            $"backup object {backupObject.Path} is already tracking in backup task {backupTask.Name}");
    }

    public static BackupTaskException NotTracking(BackupObject backupObject, BackupTask backupTask)
    {
        return new BackupTaskException(
            $"backup object {backupObject.Path} isn't tracking in backup task {backupTask.Name}");
    }

    public static BackupTaskException NotFoundInRepository(BackupObject backupObject, BackupTask backupTask)
    {
        return new BackupTaskException(
            $"backup object {backupObject.Path} is not found in backup task {backupTask.Name} repository");
    }
}