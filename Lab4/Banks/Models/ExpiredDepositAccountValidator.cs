using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Models;

public class ExpiredDepositAccountValidator : TransactionValidator
{
    private readonly int _daysToExpire;

    public ExpiredDepositAccountValidator(int daysToExpire)
    {
        if (daysToExpire < 0)
        {
            throw new NotImplementedException();
        }

        _daysToExpire = daysToExpire;
    }

    public decimal Withdraw(DepositBankAccount account, decimal moneyAmount)
    {
        Validate(account);
        return base.Withdraw(account, moneyAmount);
    }

    public override decimal Send(TransferTransaction transaction, decimal moneyAmount)
    {
        Validate(transaction.Sender);
        return base.Send(transaction, moneyAmount);
    }

    private void Validate(IBankAccount account)
    {
        if (account.CurrentDate.DayNumber - account.CreationDate.DayNumber < _daysToExpire)
        {
            throw new NotImplementedException();
        }
    }
}