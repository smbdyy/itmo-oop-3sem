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
        if (amount > accountMoney)
        {
            throw new NotImplementedException();
        }

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

    public decimal Transfer(decimal accountMoney, decimal amount, IBankAccount recipient)
    {
        ValidateMoneyAmount(amount);
        if (amount > accountMoney)
        {
            throw new NotImplementedException();
        }

        if (accountMoney > _maxWithdrawalAmount)
        {
            throw new NotImplementedException();
        }

        recipient.Replenish(amount);
        return accountMoney - amount;
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