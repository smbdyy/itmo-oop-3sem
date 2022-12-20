﻿using Banks.Builders;
using Banks.Interfaces;
using Banks.Tools.Exceptions;

namespace Banks.Entities;

public class CentralBank : ICentralBank
{
    private readonly List<BankClient> _clients = new ();
    private readonly List<IBank> _banks = new ();
    private BankBuilder _bankBuilder;

    public CentralBank(BankBuilder bankBuilder)
    {
        _bankBuilder = bankBuilder;
        CurrentDate = DateOnly.FromDateTime(DateTime.Now);
    }

    public CentralBank(BankBuilder bankBuilder, DateOnly currentDate)
    {
        _bankBuilder = bankBuilder;
        CurrentDate = currentDate;
    }

    public DateOnly CurrentDate { get; }

    public BankBuilder BankBuilder
    {
        set => _bankBuilder = value;
    }

    public IReadOnlyCollection<BankClient> Clients => _clients;

    public void SetDefaultDepositAccountTerm(int term)
    {
        _bankBuilder.SetDepositAccountTerm(term);
    }

    public void SetDefaultCreditAccountCommission(decimal commission)
    {
        _bankBuilder.SetCreditAccountCommission(commission);
    }

    public void SetDefaultCreditAccountLimit(decimal limit)
    {
        _bankBuilder.SetCreditAccountLimit(limit);
    }

    public void SetDefaultUnverifiedClientWithdrawalLimit(decimal value)
    {
        _bankBuilder.SetMaxUnverifiedClientWithdrawal(value);
    }

    public IBank CreateBank(string name)
    {
        _bankBuilder.SetName(name);
        IBank bank = _bankBuilder.Build();
        _banks.Add(bank);
        return bank;
    }

    public void DeleteBank(IBank bank)
    {
        if (!_banks.Contains(bank))
        {
            throw NotFoundException.Bank(bank);
        }

        _banks.Remove(bank);
    }

    public IBank? FindBankById(Guid id)
    {
        return _banks.FirstOrDefault(b => b.Id == id);
    }

    public void RegisterClient(BankClient client)
    {
        if (_clients.Contains(client))
        {
            throw AlreadyExistsException.ClientRegistered(client);
        }

        _clients.Add(client);
    }

    public void NotifyNextDay()
    {
        foreach (IBank bank in _banks)
        {
            bank.NotifyNextDay();
        }
    }

    public void DeleteClientAndAccounts(BankClient client)
    {
        if (!_clients.Contains(client))
        {
            throw NotFoundException.BankClient(client);
        }

        foreach (IBank bank in _banks)
        {
            bank.DeleteAllClientAccounts(client);
        }

        _clients.Remove(client);
    }
}