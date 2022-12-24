using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Tools.TransactionValidators;

public abstract class TransactionValidator
{
    private TransactionValidator? _next;

    public TransactionValidator SetNext(TransactionValidator next)
    {
        _next = next;
        return next;
    }

    public virtual decimal Withdraw(IBankAccount account, MoneyAmount moneyAmount)
    {
        return _next?.Withdraw(account, moneyAmount) ?? account.MoneyAmount;
    }

    public virtual decimal Replenish(IBankAccount account, MoneyAmount moneyAmount)
    {
        return _next?.Replenish(account, moneyAmount) ?? account.MoneyAmount;
    }

    public virtual decimal Send(TransferTransaction transaction)
    {
        return _next?.Send(transaction) ?? transaction.Sender.MoneyAmount;
    }
}