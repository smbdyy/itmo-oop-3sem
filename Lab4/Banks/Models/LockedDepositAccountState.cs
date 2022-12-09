using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Models;

public class LockedDepositAccountState : IAccountState
{
    public decimal Withdraw(decimal accountMoney, decimal amount)
    {
        throw new NotImplementedException();
    }

    public decimal Replenish(decimal accountMoney, decimal amount)
    {
        if (amount < 0)
        {
            throw new NotImplementedException();
        }

        return accountMoney + amount;
    }

    public decimal Send(TransferTransaction transaction)
    {
        throw new NotImplementedException();
    }
}