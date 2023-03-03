namespace Backups.Extra.Loggers.MessageGenerators;

public class CurrentTimePrefixGenerator : ILoggerPrefixGenerator
{
    public string GetPrefix() => DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
}