using Banks.Entities;

namespace Banks.Interfaces;

public interface IBank
{
    public Guid Id { get; }
    public string Name { get; }
    public IReadOnlyCollection<IBankAccount> Accounts { get; }
    public IReadOnlyCollection<BankClient> Clients { get; }
    public decimal DepositAccountPercent { get; }
    public int DepositAccountTerm { get; }
    public decimal CreditAccountCommission { get; }
    public DateOnly CurrentDate { get; }

    public void ToNextDay();
    public void AddAccount(IBankAccount account);
}