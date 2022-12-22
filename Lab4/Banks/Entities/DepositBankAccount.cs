﻿using Banks.Interfaces;
using Banks.Models;
using Banks.Tools.Exceptions;
using Banks.Tools.TransactionValidators;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Entities;

public class DepositBankAccount : IBankAccount
{
    private readonly List<ITransaction> _transactions = new ();
    private readonly List<ITransactionInfo> _transactionHistory = new ();
    private readonly TransactionValidator _validationChain;
    private decimal _moneyToAdd;

    public DepositBankAccount(
        BankClient client,
        decimal moneyAmount,
        decimal percent,
        int daysToExpire,
        decimal unverifiedClientWithdrawalLimit,
        DateOnly currentDate)
    {
        if (moneyAmount < 0)
        {
            throw ArgumentException.InappropriateNegativeNumber(moneyAmount);
        }

        if (percent < 0)
        {
            throw ArgumentException.InappropriateNegativeNumber(percent);
        }

        Client = client;
        MoneyAmount = moneyAmount;
        Percent = percent;
        CurrentDate = currentDate;
        CreationDate = currentDate;

        _validationChain = new ExpiredDepositAccountValidator(daysToExpire);
        _validationChain
            .SetNext(new EnoughMoneyValidator())
            .SetNext(new VerifiedClientValidator(unverifiedClientWithdrawalLimit))
            .SetNext(new TransactionFinisher());
    }

    public BankClient Client { get; }
    public decimal MoneyAmount { get; private set; }
    public decimal Percent { get; }
    public DateOnly CreationDate { get; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly CurrentDate { get; private set; } = DateOnly.FromDateTime(DateTime.Now);
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
        CurrentDate = CurrentDate.AddDays(1);
        _moneyToAdd += MoneyAmount * (Percent / 36500);
        if (CurrentDate.Day == CreationDate.Day)
        {
            MoneyAmount += _moneyToAdd;
            _moneyToAdd = 0;
        }
    }

    private ITransaction? FindTransaction(Guid id)
    {
        return _transactions.FirstOrDefault(t => t.Id == id);
    }
}