using Banks.Interfaces;

namespace Banks.Entities;

public class ReplenishmentTransaction : ITransaction
{
    public ReplenishmentTransaction(decimal amount, decimal commission)
    {
        Amount = amount;
        Commission = commission;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public decimal Amount { get; }
    public decimal Commission { get; }

    public decimal GetUndoResult(decimal accountMoney)
    {
        return accountMoney - Amount + Commission;
    }
}