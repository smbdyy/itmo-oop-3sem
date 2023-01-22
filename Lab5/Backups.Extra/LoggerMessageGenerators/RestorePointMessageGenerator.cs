using Backups.Extra.Interfaces;
using Backups.Models;

namespace Backups.Extra.LoggerMessageGenerators;

public class RestorePointMessageGenerator : ILoggerMessageGenerator
{
    private readonly IRestorePoint _restorePoint;

    public RestorePointMessageGenerator(IRestorePoint restorePoint)
    {
        _restorePoint = restorePoint;
    }

    public string GetMessage()
    {
        string message =
            $"restore point (creation time: {_restorePoint.CreationDateTime}, folder: {_restorePoint.FolderName}, objects:";
        return _restorePoint.BackupObjects
            .Aggregate(message, (current, backupObject) => current + Environment.NewLine + backupObject.Path);
    }
}