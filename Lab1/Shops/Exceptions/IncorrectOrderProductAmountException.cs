namespace Shops.Exceptions;

public class IncorrectOrderProductAmountException : Exception
{
    public IncorrectOrderProductAmountException() { }

    public IncorrectOrderProductAmountException(int amount)
    {
        Message = $"cannot order {amount} pcs of a product";
    }

    public override string Message { get; } = "minimal amount of a product in order is 1";
}