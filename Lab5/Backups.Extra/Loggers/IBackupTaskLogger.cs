namespace Backups.Extra.Loggers;

public interface IBackupTaskLogger
{
    public void WriteLog(string message);
}