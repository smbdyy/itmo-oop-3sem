using Banks.Entities;

namespace Banks.Interfaces;

public interface IAccountState
{
    public decimal Withdraw(decimal accountMoney, decimal amount);
    public decimal Replenish(decimal accountMoney, decimal amount);
    public decimal Send(TransferTransaction transaction);
}