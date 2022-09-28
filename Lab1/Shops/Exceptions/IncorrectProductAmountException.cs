namespace Shops.Exceptions;

public class IncorrectProductAmountException : Exception
{
    public override string Message { get; } = "amount of product cannot be negative";
}