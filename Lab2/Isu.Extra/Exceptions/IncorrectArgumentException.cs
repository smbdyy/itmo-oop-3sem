namespace Isu.Extra.Exceptions;

public class IncorrectArgumentException : Exception
{
    public IncorrectArgumentException() { }

    public IncorrectArgumentException(string message)
        : base(message) { }

    public static IncorrectArgumentException ClassroomBlock(int value)
    {
        return new IncorrectArgumentException($"classroom block number must be >= 1 and <= 9 ({value} is given)");
    }

    public static IncorrectArgumentException ClassroomFloor(int value)
    {
        return new IncorrectArgumentException($"classroom floor number must be >= 1 and <= 9 ({value} is given)");
    }

    public static IncorrectArgumentException ClassroomNumber(int value)
    {
        return new IncorrectArgumentException($"classroom number must be >= 1 and <= 9 ({value} is given)");
    }

    public static IncorrectArgumentException TimeIdIsLessThanOne(int value)
    {
        return new IncorrectArgumentException($"time id must be a positive number ({value} is given)");
    }

    public static IncorrectArgumentException TimeIdIsMoreThanMax(int value, int max)
    {
        return new IncorrectArgumentException($"time id {value} is more than max lessons amount ({max})");
    }

    public static IncorrectArgumentException EmptyNameString()
    {
        return new IncorrectArgumentException($"name cannot be an empty string");
    }

    public static IncorrectArgumentException MaxMembersLessThanOne(int value)
    {
        return new IncorrectArgumentException($"max members must be a positive number ({value} is given)");
    }
}