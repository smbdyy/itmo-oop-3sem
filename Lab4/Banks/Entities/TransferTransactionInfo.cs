using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class TransferTransactionInfo : ITransactionInfo
{
    public TransferTransactionInfo(TransferTransaction transaction)
    {
        TransactionId = transaction.Id;
        Amount = transaction.Amount;
        Commission = transaction.Commission;
        Sender = transaction.Sender;
        Recipient = transaction.Recipient;
        Description = $"send {Amount} to {Recipient.Client.Name.AsString}, commission {Commission}";
    }

    public Guid TransactionId { get; }
    public MoneyAmount Amount { get; }
    public MoneyAmount Commission { get; }
    public IBankAccount Sender { get; }
    public IBankAccount Recipient { get; }
    public string Description { get; }
}