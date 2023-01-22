namespace Backups.Extra.Interfaces;

public interface IBackupTaskLogger
{
    public void WriteLog(string message);
    public void WriteLog(string eventMessage, ILoggerMessageGenerator entityMessageGenerator);
}