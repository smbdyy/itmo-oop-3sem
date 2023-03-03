namespace Backups.Extra.Loggers.MessageGenerators;

public class EmptyPrefixGenerator : ILoggerPrefixGenerator
{
    public string GetPrefix() => string.Empty;
}