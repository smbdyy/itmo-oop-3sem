using Banks.Builders;
using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;
using Banks.Tools.Exceptions;
using Banks.Tools.NotificationReceivers;
using Xunit;

namespace Banks.Test;

public class BanksTest
{
    [Fact]
    public void ReplenishMoney_AccountHasMoney()
    {
        var centralBank = new CentralBank(new DefaultBankBuilder());
        IBank bank = centralBank.CreateBank("bank");
        BankClient client = new BankClientBuilder()
            .SetName(PersonName.FromString("Ivan Ivanov"))
            .AddNotificationReceiver(new ConsoleNotificationReceiver())
            .Build();
        IBankAccount account = bank.CreateDebitAccount(client);
        account.Replenish(1000);

        Assert.Equal(1000, account.MoneyAmount);
    }

    [Fact]
    public void WithdrawMoney_MoneySubtracted()
    {
        var centralBank = new CentralBank(new DefaultBankBuilder());
        IBank bank = centralBank.CreateBank("bank");
        BankClient client = new BankClientBuilder()
            .SetName(PersonName.FromString("Ivan Ivanov"))
            .AddNotificationReceiver(new ConsoleNotificationReceiver())
            .Build();
        IBankAccount account = bank.CreateDebitAccount(client);
        account.Replenish(1000);
        account.Withdraw(100);

        Assert.Equal(900, account.MoneyAmount);
    }

    [Fact]
    public void ExceedUnverifiedClientWithdrawalLimit_ExceptionThrownMoneyRemained()
    {
        var centralBank = new CentralBank(new DefaultBankBuilder());
        centralBank.SetDefaultUnverifiedClientWithdrawalLimit(500);
        IBank bank = centralBank.CreateBank("bank");
        BankClient client = new BankClientBuilder()
            .SetName(PersonName.FromString("Ivan Ivanov"))
            .AddNotificationReceiver(new ConsoleNotificationReceiver())
            .Build();
        IBankAccount account = bank.CreateDebitAccount(client);
        account.Replenish(1000);

        Assert.Throws<TransactionValidationException>(() => account.Withdraw(501));
        Assert.Equal(1000, account.MoneyAmount);
    }

    [Fact]
    public void SendMoneyFromOneAccountToAnother_MoneySentMoneyReceived()
    {
        var centralBank = new CentralBank(new DefaultBankBuilder());
        IBank bank = centralBank.CreateBank("bank");
        BankClient client = new BankClientBuilder()
            .SetName(PersonName.FromString("Ivan Ivanov"))
            .AddNotificationReceiver(new ConsoleNotificationReceiver())
            .Build();
        centralBank.RegisterClient(client);
        IBankAccount accountA = bank.CreateDebitAccount(client);
        IBankAccount accountB = bank.CreateDebitAccount(client);
        accountA.Replenish(1000);
        accountA.Send(100, accountB);

        Assert.Equal(900, accountA.MoneyAmount);
        Assert.Equal(100, accountB.MoneyAmount);
    }

    [Fact]
    public void TryWithdrawFromNonExpiredDepositAccount_ExceptionThrownMoneyRemained()
    {
        var centralBank = new CentralBank(new DefaultBankBuilder());
        IBank bank = centralBank.CreateBank("bank");
        BankClient client = new BankClientBuilder()
            .SetName(PersonName.FromString("Ivan Ivanov"))
            .AddNotificationReceiver(new ConsoleNotificationReceiver())
            .Build();
        centralBank.RegisterClient(client);
        IBankAccount account = bank.CreateDepositAccount(client, 1000);

        Assert.Throws<TransactionValidationException>(() => account.Withdraw(1));
        Assert.Equal(1000, account.MoneyAmount);
    }

    [Fact]
    public void VerifyClient_CanWithdrawWithoutLimit()
    {
        var centralBank = new CentralBank(new DefaultBankBuilder());
        centralBank.SetDefaultUnverifiedClientWithdrawalLimit(500);
        IBank bank = centralBank.CreateBank("bank");
        BankClient client = new BankClientBuilder()
            .SetName(PersonName.FromString("Ivan Ivanov"))
            .AddNotificationReceiver(new ConsoleNotificationReceiver())
            .Build();
        centralBank.RegisterClient(client);
        IBankAccount account = bank.CreateDebitAccount(client);
        account.Replenish(1000);
        client.Address = new AddressBuilder()
            .SetCountry("Russia")
            .SetTown("Saint-P")
            .SetStreet("Kronverksky Prospect")
            .SetHouseNumber("49")
            .Build();
        client.PassportNumber = PassportNumber.FromString("0000 123456");
        account.Withdraw(700);
        Assert.Equal(300, account.MoneyAmount);
    }

    [Fact]
    public void ExpireDepositAccount_CanWithdraw()
    {
        var centralBank = new CentralBank(new DefaultBankBuilder());
        centralBank.SetDefaultDepositAccountTerm(10);
        IBank bank = centralBank.CreateBank("bank");
        BankClient client = new BankClientBuilder()
            .SetName(PersonName.FromString("Ivan Ivanov"))
            .AddNotificationReceiver(new ConsoleNotificationReceiver())
            .Build();
        centralBank.RegisterClient(client);
        IBankAccount account = bank.CreateDepositAccount(client, 1000);
        for (int i = 0; i < 11; i++)
        {
            centralBank.NotifyNextDay();
        }

        account.Withdraw(100);

        Assert.Equal(900, account.MoneyAmount);
    }

    [Fact]
    public void SkipMonth_DepositAccountPercentPaid()
    {
        var centralBank = new CentralBank(new DefaultBankBuilder());
        IBank bank = centralBank.CreateBank("bank");
        bank.AddDepositAccountPercent(new StartAmountPercentPair(1000, 3.65M));
        BankClient client = new BankClientBuilder()
            .SetName(PersonName.FromString("Ivan Ivanov"))
            .AddNotificationReceiver(new ConsoleNotificationReceiver())
            .Build();
        centralBank.RegisterClient(client);
        IBankAccount account = bank.CreateDepositAccount(client, 100_000);
        for (int i = 0; i < 31; i++)
        {
            centralBank.NotifyNextDay();
        }

        Assert.Equal(100_310, account.MoneyAmount);
    }
}