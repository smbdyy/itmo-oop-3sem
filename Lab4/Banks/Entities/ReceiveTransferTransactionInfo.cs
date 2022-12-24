using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class ReceiveTransferTransactionInfo : ITransactionInfo
{
    public ReceiveTransferTransactionInfo(ReceiveTransferTransaction transaction)
    {
        TransactionId = transaction.Id;
        Amount = transaction.Amount;
        Commission = transaction.Commission;
        Sender = transaction.Sender;
        Description = $"receive {Amount} from {Sender.Client.Name.AsString}, commission {Commission}";
    }

    public Guid TransactionId { get; }
    public MoneyAmount Amount { get; }
    public MoneyAmount Commission { get; }
    public IBankAccount Sender { get; }
    public string Description { get; }
}