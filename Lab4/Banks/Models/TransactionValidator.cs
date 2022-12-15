using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Models;

public abstract class TransactionValidator
{
    private TransactionValidator? _next;

    public TransactionValidator SetNext(TransactionValidator next)
    {
        _next = next;
        return this;
    }

    public virtual decimal Withdraw(IBankAccount account, decimal moneyAmount)
    {
        return _next?.Withdraw(account, moneyAmount) ?? account.MoneyAmount;
    }

    public virtual decimal Replenish(IBankAccount account, decimal moneyAmount)
    {
        return _next?.Replenish(account, moneyAmount) ?? account.MoneyAmount;
    }

    public virtual decimal Send(TransferTransaction transaction, decimal moneyAmount)
    {
        return _next?.Send(transaction, moneyAmount) ?? transaction.Sender.MoneyAmount;
    }
}