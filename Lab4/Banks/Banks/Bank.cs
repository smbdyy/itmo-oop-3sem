using Banks.Accounts;
using Banks.Accounts.Builders;
using Banks.Builders;
using Banks.Clients;
using Banks.Models;
using Banks.Tools.Exceptions;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Banks;

public class Bank : IBank
{
    private readonly List<IBankAccount> _accounts = new ();
    private readonly List<DepositPercentInfo> _depositPercentInfo = new ();
    private readonly List<BankClient> _subscribers = new ();
    private readonly BankNotificationBuilder _bankNotificationBuilder;
    private DepositTermDays _depositAccountTerm;
    private MoneyAmount _creditAccountCommission;
    private NonPositiveMoneyAmount _creditAccountLimit;
    private MoneyAmount _unverifiedClientWithdrawalLimit;

    public Bank(
        string name,
        int depositAccountTerm,
        MoneyAmount creditAccountCommission,
        NonPositiveMoneyAmount creditAccountLimit,
        MoneyAmount unverifiedClientWithdrawalLimit,
        BankNotificationBuilder bankNotificationBuilder)
    {
        if (name == string.Empty)
        {
            throw ArgumentException.EmptyString();
        }

        _bankNotificationBuilder = bankNotificationBuilder;
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
    public IReadOnlyCollection<DepositPercentInfo> DepositPercentInfo => _depositPercentInfo;
    public IReadOnlyCollection<BankClient> Subscribers => _subscribers;
    public DepositTermDays DepositAccountTerm
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

    public void AddDepositPercentInfo(DepositPercentInfo info)
    {
        if (_depositPercentInfo.Any(p => p.StartAmount == info.StartAmount))
        {
            throw AlreadyExistsException.PairForStartAmount(info.StartAmount);
        }

        if (_depositPercentInfo.Any(p => p.Percent == info.Percent))
        {
            throw AlreadyExistsException.PairForPercent(info.Percent);
        }

        _depositPercentInfo.Add(info);
        NotifySubscribers();
    }

    public void DeleteDepositPercentInfo(DepositPercentInfo info)
    {
        DepositPercentInfo? found = _depositPercentInfo.FirstOrDefault(p =>
            p.StartAmount != info.StartAmount && p.Percent != info.Percent);
        if (found is null)
        {
            throw NotFoundException.StartAmountPercentPair(info, this);
        }

        _depositPercentInfo.Remove(found);
    }

    public IBankAccount CreateAccount(BankAccountBuilder builder)
    {
        builder.SetBank(this);
        IBankAccount account = builder.Build();
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

    private void NotifySubscribers()
    {
        _bankNotificationBuilder.SetBank(this);
        string message = _bankNotificationBuilder.GetNotificationMessage();

        foreach (BankClient client in _subscribers)
        {
            client.NotificationReceiver.Receive($"Dear {client.Name.AsString}, {Environment.NewLine}{message}");
        }
    }
}