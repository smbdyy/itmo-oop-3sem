using System.Net.NetworkInformation;
using Banks.Interfaces;
using Banks.Models;
using Banks.Tools.Exceptions;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Entities;

public class Bank : IBank
{
    private readonly List<IBankAccount> _accounts = new ();
    private readonly List<StartAmountPercentPair> _depositAmountPercentPairs = new ();
    private readonly List<BankClient> _subscribers = new ();
    private int _depositAccountTerm;
    private MoneyAmount _creditAccountCommission;
    private NonPositiveMoneyAmount _creditAccountLimit;
    private MoneyAmount _unverifiedClientWithdrawalLimit;

    public Bank(
        string name,
        int depositAccountTerm,
        MoneyAmount creditAccountCommission,
        NonPositiveMoneyAmount creditAccountLimit,
        MoneyAmount unverifiedClientWithdrawalLimit)
    {
        if (name == string.Empty)
        {
            throw ArgumentException.EmptyString();
        }

        Name = name;
        DepositAccountTerm = depositAccountTerm;
        CreditAccountCommission = creditAccountCommission;
        CreditAccountLimit = creditAccountLimit;
        UnverifiedClientWithdrawalLimit = unverifiedClientWithdrawalLimit;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }
    public DateOnly CurrentDate { get; private set; } = DateOnly.FromDateTime(DateTime.Now);
    public IReadOnlyCollection<IBankAccount> Accounts => _accounts;
    public IReadOnlyCollection<StartAmountPercentPair> StartAmountPercentPairs => _depositAmountPercentPairs;
    public IReadOnlyCollection<BankClient> Subscribers => _subscribers;
    public int DepositAccountTerm
    {
        get => _depositAccountTerm;
        set
        {
            _depositAccountTerm = value;
            NotifySubscribers();
        }
    }

    public MoneyAmount CreditAccountCommission
    {
        get => _creditAccountCommission;
        set
        {
            _creditAccountCommission = value;
            NotifySubscribers();
        }
    }

    public NonPositiveMoneyAmount CreditAccountLimit
    {
        get => _creditAccountLimit;
        set
        {
            _creditAccountLimit = value;
            NotifySubscribers();
        }
    }

    public MoneyAmount UnverifiedClientWithdrawalLimit
    {
        get => _unverifiedClientWithdrawalLimit;
        set
        {
            _unverifiedClientWithdrawalLimit = value;
            NotifySubscribers();
        }
    }

    public void NotifyNextDay()
    {
        CurrentDate = CurrentDate.AddDays(1);
        foreach (IBankAccount account in _accounts)
        {
            account.NotifyNextDay();
        }
    }

    public void AddDepositAccountPercent(StartAmountPercentPair depositAmountPercentPair)
    {
        if (_depositAmountPercentPairs.Any(p => p.StartAmount == depositAmountPercentPair.StartAmount))
        {
            throw AlreadyExistsException.PairForStartAmount(depositAmountPercentPair.StartAmount);
        }

        if (_depositAmountPercentPairs.Any(p => p.Percent == depositAmountPercentPair.Percent))
        {
            throw AlreadyExistsException.PairForPercent(depositAmountPercentPair.Percent);
        }

        _depositAmountPercentPairs.Add(depositAmountPercentPair);
        NotifySubscribers();
    }

    public void DeleteDepositAccountPercent(StartAmountPercentPair depositAmountPercentPair)
    {
        StartAmountPercentPair? found = _depositAmountPercentPairs.FirstOrDefault(p =>
            p.StartAmount != depositAmountPercentPair.StartAmount && p.Percent != depositAmountPercentPair.Percent);
        if (found is null)
        {
            throw NotFoundException.StartAmountPercentPair(depositAmountPercentPair, this);
        }

        _depositAmountPercentPairs.Remove(found);
    }

    public CreditBankAccount CreateCreditAccount(BankClient client)
    {
        var account = new CreditBankAccount(
            client,
            _creditAccountLimit,
            _creditAccountCommission,
            _unverifiedClientWithdrawalLimit,
            CurrentDate);

        _accounts.Add(account);
        return account;
    }

    public DebitBankAccount CreateDebitAccount(BankClient client)
    {
        var account = new DebitBankAccount(client, _unverifiedClientWithdrawalLimit, CurrentDate);
        _accounts.Add(account);
        return account;
    }

    public DepositBankAccount CreateDepositAccount(BankClient client, MoneyAmount startMoneyAmount)
    {
        var account = new DepositBankAccount(
            client,
            startMoneyAmount,
            CalculateDepositAccountPercent(startMoneyAmount),
            DepositAccountTerm,
            _unverifiedClientWithdrawalLimit,
            CurrentDate);

        _accounts.Add(account);
        return account;
    }

    public void DeleteAccount(IBankAccount account)
    {
        if (!_accounts.Contains(account))
        {
            throw NotFoundException.BankAccount(account);
        }

        _accounts.Remove(account);
    }

    public void DeleteAllClientAccounts(BankClient client)
    {
        List<IBankAccount> accounts = _accounts.FindAll(a => a.Client == client);

        foreach (IBankAccount account in accounts)
        {
            _accounts.Remove(account);
        }
    }

    public void SubscribeToNotifications(BankClient client)
    {
        if (_subscribers.Contains(client))
        {
            throw AlreadyExistsException.ClientSubscribed(client, this);
        }

        _subscribers.Add(client);
    }

    public void UnsubscribeFromNotifications(BankClient client)
    {
        if (!_subscribers.Contains(client))
        {
            throw NotFoundException.BankClientInBank(client, this);
        }

        _subscribers.Remove(client);
    }

    private MoneyAmount CalculateDepositAccountPercent(MoneyAmount startAmount)
    {
        MoneyAmount percent = 0;
        foreach (StartAmountPercentPair amountPercent in _depositAmountPercentPairs)
        {
            if (startAmount >= amountPercent.StartAmount)
            {
                percent = amountPercent.Percent;
            }
        }

        return percent;
    }

    private void NotifySubscribers()
    {
        string message = @$"Our terms has been updated. Current terms: 
                            Deposit account term: {_depositAccountTerm},
                            Credit account commission: {_creditAccountCommission},
                            Credit account limit: {_creditAccountLimit},
                            Unverified client withdrawal limit: {_unverifiedClientWithdrawalLimit}.
                            Deposit account percents:";
        foreach (StartAmountPercentPair pair in _depositAmountPercentPairs)
        {
            message += Environment.NewLine + $"From {pair.StartAmount}: {pair.Percent}";
        }

        foreach (BankClient client in _subscribers)
        {
            client.NotificationReceiver.Receive($"Dear {client.Name.AsString}, {Environment.NewLine}{message}");
        }
    }
}