using Banks.Clients;
using Banks.Models;
using Banks.Tools.Exceptions;
using Banks.Transactions;
using Banks.Transactions.Info;
using Banks.Transactions.Processors;

namespace Banks.Accounts;

public class CreditBankAccount : IBankAccount
{
    private readonly List<ITransaction> _transactions = new ();
    private readonly List<ITransactionInfo> _transactionHistory = new ();
    private readonly TransactionProcessor _transactionProcessingChain;

    public CreditBankAccount(
        BankClient client,
        NonPositiveMoneyAmount limit,
        MoneyAmount commission,
        MoneyAmount unverifiedClientWithdrawalLimit,
        DateOnly currentDate)
    {
        Client = client;
        Limit = limit;
        Commission = commission;
        CurrentDate = currentDate;
        CreationDate = currentDate;

        _transactionProcessingChain = new EnoughMoneyValidator();
        _transactionProcessingChain
            .SetNext(new VerifiedClientValidator(unverifiedClientWithdrawalLimit))
            .SetNext(new TransactionFinisher());
    }

    public BankClient Client { get; }
    public decimal MoneyAmount { get; private set; }
    public NonPositiveMoneyAmount Limit { get; }
    public MoneyAmount Commission { get; }
    public DateOnly CreationDate { get; }
    public DateOnly CurrentDate { get; }
    public Guid Id { get; } = Guid.NewGuid();
    public IReadOnlyCollection<ITransactionInfo> TransactionHistory => _transactionHistory;

    public void Withdraw(MoneyAmount amount)
    {
        MoneyAmount = _transactionProcessingChain.Withdraw(this, amount + Commission);
        var transaction = new WithdrawalTransaction(amount, Commission);
        _transactions.Add(transaction);
        _transactionHistory.Add(new WithdrawalTransactionInfo(transaction));
    }

    public void Replenish(MoneyAmount amount)
    {
        MoneyAmount = _transactionProcessingChain.Replenish(this, amount);
        var transaction = new ReplenishmentTransaction(amount, 0);
        _transactions.Add(transaction);
        _transactionHistory.Add(new ReplenishmentTransactionInfo(transaction));
    }

    public void Send(MoneyAmount amount, IBankAccount recipient)
    {
        if (recipient == this)
        {
            throw TransactionValidationException.RecipientIsSender();
        }

        var transaction = new TransferTransaction(amount, Commission, this, recipient);
        MoneyAmount = _transactionProcessingChain.Send(transaction);
        _transactions.Add(transaction);
        _transactionHistory.Add(new TransferSendingTransactionInfo(transaction));
    }

    public void Receive(TransferTransaction transaction)
    {
        var receiveTransaction = new TransferReceivingTransaction(transaction, 0);
        MoneyAmount = _transactionProcessingChain.Replenish(this, transaction.Amount);
        _transactions.Add(receiveTransaction);
        _transactionHistory.Add(new TransferReceivingTransactionInfo(receiveTransaction));
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