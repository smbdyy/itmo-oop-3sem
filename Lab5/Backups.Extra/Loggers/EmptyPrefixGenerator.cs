using Backups.Extra.Interfaces;

namespace Backups.Extra.Loggers;

public class EmptyPrefixGenerator : ILoggerPrefixGenerator
{
    public string GetPrefix() => string.Empty;
}