namespace Isu.Exceptions;

public class IncorrectGroupNumberException : Exception
{
    public IncorrectGroupNumberException() { }

    public IncorrectGroupNumberException(int groupNumber)
    {
        this.Message = $"group number must be >= 0 and <= 99 ({groupNumber} is given)";
    }

    public override string Message { get; } = "group number must be >= 0 and <= 99";
}