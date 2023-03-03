using Banks.Accounts;
using Banks.Models;
using Banks.Tools.Exceptions;

namespace Banks.Transactions.Processors;

public class EnoughMoneyValidator : TransactionProcessor
{
    private decimal _limit = 0;

    public EnoughMoneyValidator() { }
    public EnoughMoneyValidator(decimal limit)
    {
        _limit = limit;
    }

    public override decimal Withdraw(IBankAccount account, MoneyAmount moneyAmount)
    {
        Validate(account, moneyAmount);
        return base.Withdraw(account, moneyAmount);
    }

    public override decimal Send(TransferTransaction transaction)
    {
        Validate(transaction.Sender, transaction.Amount + transaction.Commission);
        return base.Send(transaction);
    }

    private void Validate(IBankAccount account, decimal amount)
    {
        if (account.MoneyAmount - amount < _limit)
        {
            throw TransactionValidationException.NotEnoughMoney(account, amount);
        }
    }
}