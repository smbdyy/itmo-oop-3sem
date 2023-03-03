using Banks.Accounts;
using Banks.Models;

namespace Banks.Transactions.Info;

public class TransferSendingTransactionInfo : ITransactionInfo
{
    public TransferSendingTransactionInfo(TransferTransaction transaction)
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