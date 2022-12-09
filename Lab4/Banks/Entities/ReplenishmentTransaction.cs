using Banks.Interfaces;

namespace Banks.Entities;

public class ReplenishmentTransaction : ITransaction
{
    public ReplenishmentTransaction(decimal amount)
    {
        Amount = amount;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public decimal Amount { get; }

    public decimal GetUndoResult(decimal accountMoney)
    {
        return accountMoney - Amount;
    }
}