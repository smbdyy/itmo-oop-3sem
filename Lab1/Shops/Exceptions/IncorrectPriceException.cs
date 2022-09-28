namespace Shops.Exceptions;

public class IncorrectPriceException : Exception
{
    public override string Message { get; } = "price must be a positive number";
}