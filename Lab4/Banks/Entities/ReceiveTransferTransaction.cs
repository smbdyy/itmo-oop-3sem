using Banks.Interfaces;

namespace Banks.Entities;

public class ReceiveTransferTransaction : ITransaction
{
    public ReceiveTransferTransaction(TransferTransaction transaction)
    {
        Id = transaction.Id;
        Amount = transaction.Amount;
    }

    public Guid Id { get; }
    public decimal Amount { get; }

    public decimal GetUndoResult(decimal accountMoney)
    {
        return accountMoney - Amount;
    }
}