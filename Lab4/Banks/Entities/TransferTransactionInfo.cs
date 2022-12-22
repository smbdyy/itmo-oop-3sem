using Banks.Interfaces;

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
    }

    public Guid TransactionId { get; }
    public decimal Amount { get; }
    public decimal Commission { get; }
    public IBankAccount Sender { get; }
    public IBankAccount Recipient { get; }
}