using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Models;

public struct NonPositiveMoneyAmount
{
    public NonPositiveMoneyAmount(decimal value)
    {
        if (value < 0)
        {
            throw ArgumentException.InappropriateNonNegativeNumber(value);
        }

        Value = value;
    }

    public decimal Value { get; }

    public static implicit operator decimal(NonPositiveMoneyAmount value) => value.Value;
    public static implicit operator NonPositiveMoneyAmount(decimal value) => new NonPositiveMoneyAmount(value);
}