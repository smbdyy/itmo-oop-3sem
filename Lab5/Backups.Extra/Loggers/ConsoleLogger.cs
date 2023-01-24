using Backups.Extra.Interfaces;

namespace Backups.Extra.Loggers;

public class ConsoleLogger : IBackupTaskLogger
{
    private readonly ILoggerPrefixGenerator _prefixGenerator;

    public ConsoleLogger(ILoggerPrefixGenerator prefixGenerator)
    {
        _prefixGenerator = prefixGenerator;
    }

    public void WriteLog(string message)
    {
        Console.WriteLine(message);
    }
}