using Backups.Models;
using Backups.RestorePoints;

namespace Backups.Extra.Loggers.MessageGenerators;

public static class ObjectInfoMessageGenerator
{
    public static string GetInfo(IBackupObject backupObject)
    {
        return $"backup object (path: {backupObject.Path})";
    }

    public static string GetInfo(IRestorePoint restorePoint)
    {
        string message =
            $"restore point (creation time: {restorePoint.CreationDateTime}, folder: {restorePoint.FolderName}, objects:";
        return restorePoint.BackupObjects
            .Aggregate(message, (current, backupObject) => current + Environment.NewLine + backupObject.Path);
    }
}