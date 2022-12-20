namespace Banks.Tools.Exceptions;

public class ArgumentException : Exception
{
    public ArgumentException(string message)
        : base(message) { }

    public static ArgumentException InappropriateNegativeNumber(decimal number)
    {
        return new ArgumentException($"negative number {number} cannot be used in this context");
    }

    public static ArgumentException InappropriateNegativeNumber(int number)
    {
        return new ArgumentException($"negative number {number} cannot be used in this context");
    }

    public static ArgumentException InappropriateNonNegativeNumber(decimal number)
    {
        return new ArgumentException($"non-negative number {number} cannot be used in this context");
    }

    public static ArgumentException EmptyString()
    {
        return new ArgumentException("cannot use empty string in this context");
    }

    public static ArgumentException IncorrectName(string value)
    {
        return new ArgumentException($"{value} is an incorrect name/surname");
    }
}