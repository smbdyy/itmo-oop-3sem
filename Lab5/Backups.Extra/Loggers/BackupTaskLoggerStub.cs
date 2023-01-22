using Backups.Extra.Interfaces;

namespace Backups.Extra.Loggers;

public class BackupTaskLoggerStub : IBackupTaskLogger
{
    public void WriteLog(string eventMessage, ILoggerMessageGenerator entityMessageGenerator) { }
}