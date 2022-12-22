using Banks.Interfaces;

namespace Banks.Entities;

public class ReceiveTransferTransaction : ITransaction
{
    public ReceiveTransferTransaction(TransferTransaction transaction, decimal commission)
    {
        Id = transaction.Id;
        Amount = transaction.Amount;
        Commission = commission;
        Sender = transaction.Sender;
    }

    public Guid Id { get; }
    public decimal Amount { get; }
    public decimal Commission { get; }
    public IBankAccount Sender { get; }

    public decimal GetUndoResult(decimal accountMoney)
    {
        return accountMoney - Amount + Commission;
    }
}