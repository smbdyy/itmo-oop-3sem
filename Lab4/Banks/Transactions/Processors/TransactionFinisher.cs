using Banks.Accounts;
using Banks.Models;

namespace Banks.Transactions.Processors;

public class TransactionFinisher : TransactionProcessor
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