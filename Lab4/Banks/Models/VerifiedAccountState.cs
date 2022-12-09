using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Models;

public class VerifiedAccountState : IAccountState
{
    public decimal Withdraw(decimal accountMoney, decimal amount)
    {
        ValidateMoneyAmount(amount);
        return accountMoney - amount;
    }

    public decimal Replenish(decimal accountMoney, decimal amount)
    {
        ValidateMoneyAmount(amount);
        return accountMoney + amount;
    }

    public decimal Send(TransferTransaction transaction)
    {
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