﻿using Banks.Interfaces;
using Banks.Models;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Builders;

public abstract class BankBuilder
{
    protected string? Name { get; private set; } = null;
    protected DepositTermDays DepositAccountTerm { get; private set; } = 10;
    protected MoneyAmount CreditAccountCommission { get; private set; } = 10;
    protected NonPositiveMoneyAmount CreditAccountLimit { get; private set; } = -1000;
    protected MoneyAmount MaxUnverifiedClientWithdrawal { get; private set; } = 1000;
    public abstract IBank Build();
    public void Reset()
    {
        DepositAccountTerm = 10;
        CreditAccountCommission = 10;
        CreditAccountLimit = -1000;
        MaxUnverifiedClientWithdrawal = 1000;
    }

    public BankBuilder SetName(string name)
    {
        if (name == string.Empty)
        {
            throw ArgumentException.EmptyString();
        }

        Name = name;
        return this;
    }

    public BankBuilder SetDepositAccountTerm(DepositTermDays term)
    {
        DepositAccountTerm = term;
        return this;
    }

    public BankBuilder SetCreditAccountCommission(MoneyAmount commission)
    {
        CreditAccountCommission = commission;
        return this;
    }

    public BankBuilder SetCreditAccountLimit(NonPositiveMoneyAmount limit)
    {
        CreditAccountLimit = limit;
        return this;
    }

    public BankBuilder SetMaxUnverifiedClientWithdrawal(MoneyAmount value)
    {
        MaxUnverifiedClientWithdrawal = value;
        return this;
    }
}