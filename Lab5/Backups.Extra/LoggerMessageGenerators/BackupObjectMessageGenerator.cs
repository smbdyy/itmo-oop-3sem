using Backups.Extra.Interfaces;
using Backups.Models;

namespace Backups.Extra.LoggerMessageGenerators;

public class BackupObjectMessageGenerator : ILoggerMessageGenerator
{
    private readonly IBackupObject _backupObject;

    public BackupObjectMessageGenerator(IBackupObject backupObject)
    {
        _backupObject = backupObject;
    }

    public string GetMessage()
    {
        return $"backup object (path: {_backupObject.Path})";
    }
}