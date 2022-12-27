using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Tools.TransactionValidators;

public class TransactionFinisher : TransactionValidator
{
    public override decimal Withdraw(IBankAccount account, MoneyAmount moneyAmount)
    {
        return base.Withdraw(account, moneyAmount) - moneyAmount;
    }

    public override decimal Replenish(IBankAccount account, MoneyAmount moneyAmount)
    {
        return base.Replenish(account, moneyAmount) + moneyAmount;
    }

    public override decimal Send(TransferTransaction transaction)
    {
        transaction.Recipient.Receive(transaction);
        return base.Send(transaction) - transaction.Amount - transaction.Commission;
    }
}