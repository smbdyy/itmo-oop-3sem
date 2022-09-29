namespace Shops.Exceptions;

public class IncorrectMoneyAmountException : Exception
{
    public IncorrectMoneyAmountException() { }
    public IncorrectMoneyAmountException(decimal money)
    {
        Message = $"person's money amount cannot be {money}";
    }

    public override string Message { get; } = "person's money amount cannot be negative";
}