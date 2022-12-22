using Banks.Interfaces;

namespace Banks.Entities;

public class ReceiveTransferTransactionInfo : ITransactionInfo
{
    public ReceiveTransferTransactionInfo(ReceiveTransferTransaction transaction)
    {
        TransactionId = transaction.Id;
        Amount = transaction.Amount;
        Commission = transaction.Commission;
    }

    public Guid TransactionId { get; }
    public decimal Amount { get; }
    public decimal Commission { get; }
}