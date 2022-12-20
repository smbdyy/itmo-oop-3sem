using Banks.Interfaces;
using Banks.Models;
using Banks.Tools.Exceptions;
using Banks.Tools.TransactionValidators;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Entities;

public class CreditBankAccount : IBankAccount
{
    private readonly List<ITransaction> _transactions = new ();
    private readonly TransactionValidator _validationChain;

    public CreditBankAccount(
        BankClient client,
        decimal limit,
        decimal commission,
        decimal unverifiedClientWithdrawalLimit,
        DateOnly currentDate)
    {
        if (limit > 0)
        {
            throw ArgumentException.InappropriateNonNegativeNumber(limit);
        }

        if (commission < 0)
        {
            throw ArgumentException.InappropriateNegativeNumber(commission);
        }

        Client = client;
        Limit = limit;
        Commission = commission;
        CurrentDate = currentDate;
        CreationDate = currentDate;

        _validationChain = new EnoughMoneyValidator();
        _validationChain
            .SetNext(new VerifiedClientValidator(unverifiedClientWithdrawalLimit))
            .SetNext(new TransactionFinisher());
    }

    public BankClient Client { get; }
    public decimal MoneyAmount { get; private set; }
    public decimal Limit { get; }
    public decimal Commission { get; }
    public DateOnly CreationDate { get; }
    public DateOnly CurrentDate { get; }
    public Guid Id { get; } = Guid.NewGuid();

    public void Withdraw(decimal amount)
    {
        MoneyAmount = _validationChain.Withdraw(this, amount + Commission);
        _transactions.Add(new WithdrawalTransaction(amount, Commission));
    }

    public void Replenish(decimal amount)
    {
        MoneyAmount = _validationChain.Replenish(this, amount);
        _transactions.Add(new ReplenishmentTransaction(amount, 0));
    }

    public void Send(decimal amount, IBankAccount recipient)
    {
        var transaction = new TransferTransaction(amount, Commission, this, recipient);
        MoneyAmount = _validationChain.Send(transaction);
        _transactions.Add(transaction);
    }

    public void Receive(TransferTransaction transaction)
    {
        var receiveTransaction = new ReceiveTransferTransaction(transaction, 0);
        MoneyAmount = _validationChain.Replenish(this, transaction.Amount);
        _transactions.Add(receiveTransaction);
    }

    public void Undo(Guid transactionId)
    {
        ITransaction? transaction = FindTransaction(transactionId);
        if (transaction is null)
        {
            throw NotFoundException.Transaction(transactionId, this);
        }

        MoneyAmount = transaction.GetUndoResult(MoneyAmount);
        _transactions.Remove(transaction);
    }

    public ITransaction? FindTransaction(Guid id)
    {
        return _transactions.FirstOrDefault(t => t.Id == id);
    }

    public void NotifyNextDay()
    {
        CurrentDate.AddDays(1);
    }
}