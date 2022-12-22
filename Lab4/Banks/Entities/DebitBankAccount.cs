using Banks.Interfaces;
using Banks.Models;
using Banks.Tools.Exceptions;
using Banks.Tools.TransactionValidators;

namespace Banks.Entities;

public class DebitBankAccount : IBankAccount
{
    private readonly List<ITransaction> _transactions = new ();
    private readonly List<ITransactionInfo> _transactionHistory = new ();
    private readonly TransactionValidator _validationChain;

    public DebitBankAccount(BankClient client, decimal unverifiedClientWithdrawalLimit, DateOnly currentDate)
    {
        Client = client;
        CurrentDate = currentDate;
        CreationDate = currentDate;

        _validationChain = new EnoughMoneyValidator();
        _validationChain
            .SetNext(new VerifiedClientValidator(unverifiedClientWithdrawalLimit))
            .SetNext(new TransactionFinisher());
    }

    public BankClient Client { get; }
    public decimal MoneyAmount { get; private set; }
    public DateOnly CreationDate { get; }
    public DateOnly CurrentDate { get; }
    public Guid Id { get; } = Guid.NewGuid();
    public IReadOnlyCollection<ITransactionInfo> TransactionHistory => _transactionHistory;

    public void Withdraw(decimal amount)
    {
        MoneyAmount = _validationChain.Withdraw(this, amount);
        var transaction = new WithdrawalTransaction(amount, 0);
        _transactions.Add(transaction);
        _transactionHistory.Add(new WithdrawalTransactionInfo(transaction));
    }

    public void Replenish(decimal amount)
    {
        MoneyAmount = _validationChain.Replenish(this, amount);
        var transaction = new ReplenishmentTransaction(amount, 0);
        _transactions.Add(transaction);
        _transactionHistory.Add(new ReplenishmentTransactionInfo(transaction));
    }

    public void Send(decimal amount, IBankAccount recipient)
    {
        var transaction = new TransferTransaction(amount, 0, this, recipient);
        MoneyAmount = _validationChain.Send(transaction);
        _transactions.Add(transaction);
        _transactionHistory.Add(new TransferTransactionInfo(transaction));
    }

    public void Receive(TransferTransaction transaction)
    {
        var receiveTransaction = new ReceiveTransferTransaction(transaction, 0);
        MoneyAmount = _validationChain.Replenish(this, transaction.Amount);
        _transactions.Add(receiveTransaction);
        _transactionHistory.Add(new ReceiveTransferTransactionInfo(receiveTransaction));
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

    public void NotifyNextDay()
    {
        CurrentDate.AddDays(1);
    }

    private ITransaction? FindTransaction(Guid id)
    {
        return _transactions.FirstOrDefault(t => t.Id == id);
    }
}