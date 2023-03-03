using Banks.Accounts;
using Banks.Models;

namespace Banks.Transactions.Info;

public class TransferReceivingTransactionInfo : ITransactionInfo
{
    public TransferReceivingTransactionInfo(TransferReceivingTransaction transaction)
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