using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Models;

public class UnverifiedAccountState : IAccountState
{
    private decimal _maxWithdrawalAmount;

    public UnverifiedAccountState(decimal maxWithdrawalAmount)
    {
        _maxWithdrawalAmount = ValidateMoneyAmount(maxWithdrawalAmount);
    }

    public decimal Withdraw(decimal accountMoney, decimal amount)
    {
        ValidateMoneyAmount(amount);

        if (amount > _maxWithdrawalAmount)
        {
            throw new NotImplementedException();
        }

        return accountMoney - amount;
    }

    public decimal Replenish(decimal accountMoney, decimal amount)
    {
        ValidateMoneyAmount(amount);
        return accountMoney + amount;
    }

    public decimal Send(TransferTransaction transaction)
    {
        if (transaction.Sender.MoneyAmount > _maxWithdrawalAmount)
        {
            throw new NotImplementedException();
        }

        transaction.Recipient.Receive(transaction);
        return transaction.Sender.MoneyAmount - transaction.Amount;
    }

    private static decimal ValidateMoneyAmount(decimal value)
    {
        if (value < 0)
        {
            throw new NotImplementedException();
        }

        return value;
    }
}