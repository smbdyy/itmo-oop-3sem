using Banks.Interfaces;

namespace Banks.Entities;

public class ReceiveTransferTransaction : ITransaction
{
    public ReceiveTransferTransaction(TransferTransaction transaction, decimal commission)
    {
        Id = transaction.Id;
        Amount = transaction.Amount;
        Commission = commission;
    }

    public Guid Id { get; }
    public decimal Amount { get; }
    public decimal Commission { get; }

    public decimal GetUndoResult(decimal accountMoney)
    {
        return accountMoney - Amount + Commission;
    }
}