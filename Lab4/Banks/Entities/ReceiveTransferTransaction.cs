using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class ReceiveTransferTransaction : ITransaction
{
    public ReceiveTransferTransaction(TransferTransaction transaction, MoneyAmount commission)
    {
        Id = transaction.Id;
        Amount = transaction.Amount;
        Commission = commission;
        Sender = transaction.Sender;
    }

    public Guid Id { get; }
    public MoneyAmount Amount { get; }
    public MoneyAmount Commission { get; }
    public IBankAccount Sender { get; }

    public decimal GetUndoResult(decimal accountMoney)
    {
        return accountMoney - Amount + Commission;
    }
}