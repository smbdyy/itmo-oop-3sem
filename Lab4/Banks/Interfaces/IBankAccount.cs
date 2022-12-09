using Banks.Entities;

namespace Banks.Interfaces;

public interface IBankAccount
{
    public BankClient Client { get; }
    public decimal MoneyAmount { get; }
    public decimal Withdraw(decimal amount);
    public void Replenish(decimal amount);
    public void Transfer(decimal amount, IBankAccount recipient);
}