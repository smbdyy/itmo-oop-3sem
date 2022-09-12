namespace Isu.Exceptions;

public class IncorrectMaxStudentsAmountException : Exception
{
    public IncorrectMaxStudentsAmountException() { }

    public IncorrectMaxStudentsAmountException(int maxStudentsAmount)
    {
        Message = $"max students amount must be at least 1 ({maxStudentsAmount} is given)";
    }

    public override string Message { get; } = "max students amount must be at least 1";
}