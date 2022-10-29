using Shops.Entities;

namespace Shops.Exceptions;

public class IncorrectArgumentException : Exception
{
    public IncorrectArgumentException() { }

    public IncorrectArgumentException(string message)
        : base(message) { }

    public static IncorrectArgumentException EmptyName()
    {
        return new IncorrectArgumentException("name cannot be an empty string");
    }

    public static IncorrectArgumentException IncorrectMoneyAmount(decimal amount)
    {
        return new IncorrectArgumentException($"money amount cannot be a negative number ({amount} is given)");
    }

    public static IncorrectArgumentException IncorrectPrice(decimal price)
    {
        return new IncorrectArgumentException($"price must be a positive number ({price} is given)");
    }

    public static IncorrectArgumentException IncorrectProductAmount(int amount)
    {
        return new IncorrectArgumentException("product amount cannot be a negative number ({amount} is given)");
    }
}