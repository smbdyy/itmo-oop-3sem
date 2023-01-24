using Backups.Extra.Interfaces;

namespace Backups.Extra.Loggers;

public class CurrentTimePrefixGenerator : ILoggerPrefixGenerator
{
    public string GetPrefix() => DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
}