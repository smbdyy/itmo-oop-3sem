﻿using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class DepositBankAccount : IBankAccount
{
    private readonly List<ITransaction> _transactions = new ();
    private readonly TransactionValidator _validationChain;
    private decimal _moneyToAdd;

    public DepositBankAccount(
        BankClient client,
        decimal moneyAmount,
        decimal percent,
        int daysToExpire,
        decimal maxUnverifiedClientWithdrawal,
        DateOnly currentDate)
    {
        if (moneyAmount < 0)
        {
            throw new NotImplementedException();
        }

        if (percent < 0)
        {
            throw new NotImplementedException();
        }

        Client = client;
        MoneyAmount = moneyAmount;
        Percent = percent;
        CurrentDate = currentDate;
        CreationDate = currentDate;

        _validationChain = new ExpiredDepositAccountValidator(daysToExpire)
            .SetNext(new EnoughMoneyValidator())
            .SetNext(new VerifiedClientValidator(maxUnverifiedClientWithdrawal))
            .SetNext(new TransactionFinisher());
    }

    public BankClient Client { get; }
    public decimal MoneyAmount { get; private set; }
    public decimal Percent { get; }
    public DateOnly CreationDate { get; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly CurrentDate { get; } = DateOnly.FromDateTime(DateTime.Now);

    public void Withdraw(decimal amount)
    {
        MoneyAmount = _validationChain.Withdraw(this, amount);
        _transactions.Add(new WithdrawalTransaction(amount, 0));
    }

    public void Replenish(decimal amount)
    {
        MoneyAmount = _validationChain.Replenish(this, amount);
        _transactions.Add(new ReplenishmentTransaction(amount, 0));
    }

    public void Send(decimal amount, IBankAccount recipient)
    {
        if (amount > MoneyAmount)
        {
            throw new NotImplementedException();
        }

        var transaction = new TransferTransaction(amount, 0, this, recipient);
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
        _moneyToAdd += MoneyAmount * (Percent / 36500);
        if (CurrentDate.Day == CreationDate.Day)
        {
            MoneyAmount += _moneyToAdd;
            _moneyToAdd = 0;
        }
    }
}