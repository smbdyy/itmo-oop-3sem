using Banks.Interfaces;

namespace Banks.Entities;

public class WithdrawalTransaction : ITransaction
{
    public WithdrawalTransaction(decimal amount)
    {
        Amount = amount;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public decimal Amount { get; }

    public decimal GetUndoResult(decimal accountMoney)
    {
        return accountMoney + Amount;
    }
}