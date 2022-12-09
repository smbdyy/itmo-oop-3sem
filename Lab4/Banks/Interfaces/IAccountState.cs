namespace Banks.Interfaces;

public interface IAccountState
{
    public decimal Withdraw(decimal accountMoney, decimal amount);
    public decimal Replenish(decimal accountMoney, decimal amount);
    public decimal Transfer(decimal accountMoney, decimal amount, IBankAccount recipient);
}