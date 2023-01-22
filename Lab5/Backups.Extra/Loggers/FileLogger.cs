using Backups.Extra.Interfaces;

namespace Backups.Extra.Loggers;

public class FileLogger : IBackupTaskLogger
{
    private readonly ILoggerPrefixGenerator _prefixGenerator;
    private readonly string _filePath;

    public FileLogger(ILoggerPrefixGenerator prefixGenerator, string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new NotImplementedException();
        }

        _prefixGenerator = prefixGenerator;
        _filePath = filePath;
    }

    public void WriteLog(string eventMessage, ILoggerMessageGenerator entityMessageGenerator)
    {
        using var writer = new StreamWriter(_filePath);
        writer.WriteLine($"{_prefixGenerator.GetPrefix()} {eventMessage} {entityMessageGenerator.GetMessage()}");
    }
}