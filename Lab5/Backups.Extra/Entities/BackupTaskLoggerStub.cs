using Backups.Extra.Interfaces;

namespace Backups.Extra.Entities;

public class BackupTaskLoggerStub : IBackupTaskLogger
{
    public void WriteLog(string eventMessage, ILoggerMessageGenerator entityMessageGenerator) { }
}