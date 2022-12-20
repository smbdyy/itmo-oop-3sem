using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Tools.TransactionValidators;

public class EnoughMoneyValidator : TransactionValidator
{
    private decimal _limit = 0;

    public EnoughMoneyValidator() { }
    public EnoughMoneyValidator(decimal limit)
    {
        _limit = limit;
    }

    public override decimal Withdraw(IBankAccount account, decimal moneyAmount)
    {
        Validate(account.MoneyAmount, moneyAmount);
        return base.Withdraw(account, moneyAmount);
    }

    public override decimal Send(TransferTransaction transaction)
    {
        Validate(transaction.Sender.MoneyAmount, transaction.Amount + transaction.Commission);
        return base.Send(transaction);
    }

    private void Validate(decimal accountMoney, decimal amount)
    {
        if (accountMoney - amount < _limit)
        {
            throw new NotImplementedException();
        }
    }
}