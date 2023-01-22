using Backups.Extra.Interfaces;

namespace Backups.Extra.LoggerMessageGenerators;

public class EmptyPrefixGenerator : ILoggerPrefixGenerator
{
    public string GetPrefix() => string.Empty;
}