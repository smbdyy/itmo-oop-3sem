using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Models;

public struct MoneyAmount
{
    public MoneyAmount(decimal value)
    {
        if (value < 0)
        {
            throw ArgumentException.InappropriateNegativeNumber(value);
        }

        Value = value;
    }

    public decimal Value { get; }

    public static implicit operator decimal(MoneyAmount value) => value.Value;
    public static implicit operator MoneyAmount(decimal value) => new MoneyAmount(value);
}