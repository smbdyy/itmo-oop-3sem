using Banks.Interfaces;
using Banks.Models;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Entities;

public class TransferTransaction : ITransaction
{
    public TransferTransaction(MoneyAmount amount, MoneyAmount commission, IBankAccount sender, IBankAccount recipient)
    {
        if (amount < 0)
        {
            throw ArgumentException.InappropriateNegativeNumber(amount);
        }

        if (commission < 0)
        {
            throw ArgumentException.InappropriateNegativeNumber(commission);
        }

        Amount = amount;
        Sender = sender;
        Recipient = recipient;
        Commission = commission;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public MoneyAmount Amount { get; }
    public MoneyAmount Commission { get; }
    public IBankAccount Sender { get; }
    public IBankAccount Recipient { get; }

    public decimal GetUndoResult(decimal accountMoney)
    {
        Recipient.Undo(Id);
        return accountMoney + Amount + Commission;
    }
}