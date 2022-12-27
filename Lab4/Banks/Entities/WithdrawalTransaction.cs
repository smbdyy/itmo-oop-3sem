using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class WithdrawalTransaction : ITransaction
{
    public WithdrawalTransaction(MoneyAmount amount, decimal commission)
    {
        Amount = amount;
        Commission = commission;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public MoneyAmount Amount { get; }
    public MoneyAmount Commission { get; }

    public decimal GetUndoResult(decimal accountMoney)
    {
        return accountMoney + Amount + Commission;
    }
}