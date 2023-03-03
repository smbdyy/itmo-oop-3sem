using Banks.Accounts;
using Banks.Models;

namespace Banks.Transactions.Processors;

public abstract class TransactionProcessor
{
    private TransactionProcessor? _next;

    public TransactionProcessor SetNext(TransactionProcessor next)
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