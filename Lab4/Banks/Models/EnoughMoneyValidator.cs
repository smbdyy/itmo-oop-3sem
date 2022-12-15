using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Models;

public class EnoughMoneyValidator : TransactionValidator
{
    private decimal _limit = 0;

    public EnoughMoneyValidator(decimal limit)
    {
        _limit = limit;
    }

    public override decimal Withdraw(IBankAccount account, decimal moneyAmount)
    {
        Validate(account.MoneyAmount, moneyAmount);
        return base.Withdraw(account, moneyAmount);
    }

    public override decimal Send(TransferTransaction transaction, decimal moneyAmount)
    {
        Validate(transaction.Sender.MoneyAmount, moneyAmount);
        return base.Send(transaction, moneyAmount);
    }

    private void Validate(decimal accountMoney, decimal amount)
    {
        if (accountMoney - amount < _limit)
        {
            throw new NotImplementedException();
        }
    }
}