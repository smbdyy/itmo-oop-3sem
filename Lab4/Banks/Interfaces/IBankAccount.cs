using Banks.Entities;

namespace Banks.Interfaces;

public interface IBankAccount
{
    public BankClient Client { get; }
    public decimal MoneyAmount { get; }
    public DateOnly CurrentDate { get; }
    public DateOnly CreationDate { get; }
    public Guid Id { get; }
    public void Withdraw(decimal amount);
    public void Replenish(decimal amount);
    public void Send(decimal amount, IBankAccount recipient);
    public void Receive(TransferTransaction transaction);
    public void Undo(Guid transactionId);
    public ITransaction? FindTransaction(Guid id);
    public void NotifyNextDay();
}