using Banks.Interfaces;

namespace Banks.Entities;

public class TransferTransaction : ITransaction
{
    public TransferTransaction(decimal amount, decimal commission, IBankAccount sender, IBankAccount recipient)
    {
        if (amount < 0)
        {
            throw new NotImplementedException();
        }

        Amount = amount;
        Sender = sender;
        Recipient = recipient;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public decimal Amount { get; }
    public decimal Commission { get; }
    public IBankAccount Sender { get; }
    public IBankAccount Recipient { get; }

    public decimal GetUndoResult(decimal accountMoney)
    {
        Recipient.Undo(Id);
        return accountMoney + Amount + Commission;
    }
}