using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Models;

public struct DepositTermDays
{
    public DepositTermDays(int value)
    {
        if (value < 0)
        {
            throw ArgumentException.InappropriateNegativeNumber(value);
        }

        Value = value;
    }

    public int Value { get; }

    public static implicit operator int(DepositTermDays value) => value.Value;
    public static implicit operator DepositTermDays(int value) => new DepositTermDays(value);
}