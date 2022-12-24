﻿using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Builders;

public class DepositBankAccountBuilder : BankAccountBuilder
{
    private readonly MoneyAmount _startMoneyAmount;

    public DepositBankAccountBuilder(MoneyAmount startMoneyAmount)
    {
        _startMoneyAmount = startMoneyAmount;
    }

    public override IBankAccount Build()
    {
        ValidateNotNull();
        return new DepositBankAccount(
            Client!,
            _startMoneyAmount,
            CalculateDepositAccountPercent(),
            Bank!.DepositAccountTerm,
            Bank.UnverifiedClientWithdrawalLimit,
            Bank.CurrentDate);
    }

    private MoneyAmount CalculateDepositAccountPercent()
    {
        StartAmountPercentPair? pair = Bank!.StartAmountPercentPairs
            .Where(pair => pair.StartAmount <= _startMoneyAmount)
            .MaxBy(pair => pair.StartAmount);

        return pair?.Percent ?? 0;
    }
}