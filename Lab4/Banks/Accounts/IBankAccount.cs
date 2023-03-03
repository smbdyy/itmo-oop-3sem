using Banks.Clients;
using Banks.Models;
using Banks.Transactions;
using Banks.Transactions.Info;

namespace Banks.Accounts;

public interface IBankAccount
{
    public BankClient Client { get; }
    public decimal MoneyAmount { get; }
    public DateOnly CurrentDate { get; }
    public DateOnly CreationDate { get; }
    public Guid Id { get; }
    public IReadOnlyCollection<ITransactionInfo> TransactionHistory { get; }
    public void Withdraw(MoneyAmount amount);
    public void Replenish(MoneyAmount amount);
    public void Send(MoneyAmount amount, IBankAccount recipient);
    public void Receive(TransferTransaction transaction);
    public void Undo(Guid transactionId);
    public void NotifyNextDay();
}