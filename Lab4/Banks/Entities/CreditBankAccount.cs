using Banks.Interfaces;
using Banks.Models;
using Banks.Tools.TransactionValidators;

namespace Banks.Entities;

public class CreditBankAccount : IBankAccount
{
    private readonly List<ITransaction> _transactions = new ();
    private readonly TransactionValidator _validationChain;

    public CreditBankAccount(
        BankClient client,
        decimal limit,
        decimal commission,
        decimal maxUnverifiedClientWithdrawal,
        DateOnly currentDate)
    {
        if (limit > 0)
        {
            throw new NotImplementedException();
        }

        if (commission < 0)
        {
            throw new NotImplementedException();
        }

        Client = client;
        Limit = limit;
        Commission = commission;
        CurrentDate = currentDate;
        CreationDate = currentDate;

        _validationChain = new EnoughMoneyValidator()
            .SetNext(new VerifiedClientValidator(maxUnverifiedClientWithdrawal))
            .SetNext(new TransactionFinisher());
    }

    public BankClient Client { get; }
    public decimal MoneyAmount { get; private set; }
    public decimal Limit { get; }
    public decimal Commission { get; }
    public DateOnly CreationDate { get; }
    public DateOnly CurrentDate { get; }

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
            throw new NotImplementedException();
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