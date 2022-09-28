namespace Shops.Exceptions;

public class EmptyNameStringException : Exception
{
    public override string Message { get; } = "name cannot be an empty string";
}