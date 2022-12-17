﻿using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class Bank : IBank
{
    private readonly List<IBankAccount> _accounts = new ();
    private readonly List<StartAmountPercentPair> _depositAmountPercentPairs = new ();
    private int _depositAccountTerm;
    private decimal _creditAccountCommission;
    private decimal _creditAccountLimit;
    private decimal _maxUnverifiedClientWithdrawal;

    public Bank(
        string name,
        int depositAccountTerm,
        decimal creditAccountCommission,
        decimal creditAccountLimit,
        decimal maxUnverifiedClientWithdrawal)
    {
        if (name == string.Empty)
        {
            throw new NotImplementedException();
        }

        Name = name;
        DepositAccountTerm = depositAccountTerm;
        CreditAccountCommission = creditAccountCommission;
        CreditAccountLimit = creditAccountLimit;
        MaxUnverifiedClientWithdrawal = maxUnverifiedClientWithdrawal;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }
    public DateOnly CurrentDate { get; private set; } = DateOnly.FromDateTime(DateTime.Now);
    public IReadOnlyCollection<IBankAccount> Accounts => _accounts;
    public IReadOnlyCollection<StartAmountPercentPair> StartAmountPercentPairs => _depositAmountPercentPairs;
    public int DepositAccountTerm
    {
        get => _depositAccountTerm;
        set => _depositAccountTerm = ValidateNotNegative(value);
    }

    public decimal CreditAccountCommission
    {
        get => _creditAccountCommission;
        set => _creditAccountCommission = ValidateNotNegative(value);
    }

    public decimal CreditAccountLimit
    {
        get => _creditAccountLimit;
        set => _creditAccountLimit = ValidateNotNegative(value);
    }

    public decimal MaxUnverifiedClientWithdrawal
    {
        get => _maxUnverifiedClientWithdrawal;
        set => _maxUnverifiedClientWithdrawal = ValidateNotNegative(value);
    }

    public void NotifyNextDay()
    {
        CurrentDate.AddDays(1);
        foreach (IBankAccount account in _accounts)
        {
            account.NotifyNextDay();
        }
    }

    public void AddDepositAccountPercent(StartAmountPercentPair depositAmountPercentPair)
    {
        if (_depositAmountPercentPairs.Any(p => p.StartAmount == depositAmountPercentPair.StartAmount))
        {
            throw new NotImplementedException();
        }

        if (_depositAmountPercentPairs.Any(p => p.Percent == depositAmountPercentPair.Percent))
        {
            throw new NotImplementedException();
        }

        _depositAmountPercentPairs.Add(depositAmountPercentPair);
    }

    public void DeleteDepositAccountPercent(StartAmountPercentPair depositAmountPercentPair)
    {
        StartAmountPercentPair? found = _depositAmountPercentPairs.FirstOrDefault(p =>
            p.StartAmount != depositAmountPercentPair.StartAmount && p.Percent != depositAmountPercentPair.Percent);
        if (found is null)
        {
            throw new NotImplementedException();
        }

        _depositAmountPercentPairs.Remove(found);
    }

    public CreditBankAccount CreateCreditAccount(BankClient client)
    {
        var account = new CreditBankAccount(
            client,
            _creditAccountLimit,
            _creditAccountCommission,
            _maxUnverifiedClientWithdrawal,
            CurrentDate);

        _accounts.Add(account);
        return account;
    }

    public DebitBankAccount CreateDebitAccount(BankClient client)
    {
        var account = new DebitBankAccount(client, _maxUnverifiedClientWithdrawal, CurrentDate);
        _accounts.Add(account);
        return account;
    }

    public DepositBankAccount CreateDepositAccount(BankClient client, decimal startMoneyAmount)
    {
        var account = new DepositBankAccount(
            client,
            startMoneyAmount,
            CalculateDepositAccountPercent(startMoneyAmount),
            DepositAccountTerm,
            _maxUnverifiedClientWithdrawal,
            CurrentDate);

        _accounts.Add(account);
        return account;
    }

    private static decimal ValidateNotNegative(decimal value)
    {
        if (value < 0)
        {
            throw new NotImplementedException();
        }

        return value;
    }

    private static int ValidateNotNegative(int value)
    {
        if (value < 0)
        {
            throw new NotImplementedException();
        }

        return value;
    }

    private decimal CalculateDepositAccountPercent(decimal startAmount)
    {
        if (_depositAmountPercentPairs.Count == 0)
        {
            throw new NotImplementedException();
        }

        decimal percent = 0;
        foreach (StartAmountPercentPair amountPercent in _depositAmountPercentPairs)
        {
            if (startAmount >= amountPercent.StartAmount)
            {
                percent = amountPercent.Percent;
            }
        }

        return percent;
    }
}