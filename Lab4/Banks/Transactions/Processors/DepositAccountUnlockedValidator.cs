using Banks.Accounts;
using Banks.Models;
using Banks.Tools.Exceptions;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Transactions.Processors;

public class DepositAccountUnlockedValidator : TransactionProcessor
{
    private readonly int _daysToUnlock;

    public DepositAccountUnlockedValidator(int daysToUnlock)
    {
        if (daysToUnlock < 0)
        {
            throw ArgumentException.InappropriateNegativeNumber(daysToUnlock);
        }

        _daysToUnlock = daysToUnlock;
    }

    public override decimal Withdraw(IBankAccount account, MoneyAmount moneyAmount)
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
        if (account.CurrentDate.DayNumber - account.CreationDate.DayNumber < _daysToUnlock)
        {
            throw TransactionValidationException.DepositAccountNotExpired(account, _daysToUnlock);
        }
    }
}