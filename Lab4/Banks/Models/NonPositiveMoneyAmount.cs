namespace Banks.Models;

public struct NonPositiveMoneyAmount
{
    public NonPositiveMoneyAmount(decimal value)
    {
        Value = value;
    }

    public decimal Value { get; }

    public static implicit operator decimal(NonPositiveMoneyAmount value) => value.Value;
    public static implicit operator NonPositiveMoneyAmount(decimal value) => new NonPositiveMoneyAmount(value);
}