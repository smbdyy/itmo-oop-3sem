using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;
using Banks.Tools.Exceptions;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Tools.TransactionValidators;

public class ExpiredDepositAccountValidator : TransactionValidator
{
    private readonly int _daysToExpire;

    public ExpiredDepositAccountValidator(int daysToExpire)
    {
        if (daysToExpire < 0)
        {
            throw ArgumentException.InappropriateNegativeNumber(daysToExpire);
        }

        _daysToExpire = daysToExpire;
    }

    public override decimal Withdraw(IBankAccount account, decimal moneyAmount)
    {
        Validate(account);
        return base.Withdraw(account, moneyAmount);
    }

    public override decimal Send(TransferTransaction transaction)
    {
        Validate(transaction.Sender);
        return base.Send(transaction);
    }

    private void Validate(IBankAccount account)
    {
        if (account.CurrentDate.DayNumber - account.CreationDate.DayNumber < _daysToExpire)
        {
            throw TransactionValidationException.DepositAccountNotExpired(account, _daysToExpire);
        }
    }
}