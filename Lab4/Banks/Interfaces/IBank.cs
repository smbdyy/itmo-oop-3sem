using Banks.Entities;
using Banks.Models;

namespace Banks.Interfaces;

public interface IBank
{
    public Guid Id { get; }
    public string Name { get; }
    public IReadOnlyCollection<IBankAccount> Accounts { get; }
    public IReadOnlyCollection<StartAmountPercentPair> StartAmountPercentPairs { get; }
    public IReadOnlyCollection<BankClient> Subscribers { get; }
    public int DepositAccountTerm { get; set; }
    public MoneyAmount CreditAccountCommission { get; set; }
    public NonPositiveMoneyAmount CreditAccountLimit { get; set; }
    public MoneyAmount UnverifiedClientWithdrawalLimit { get; set; }
    public DateOnly CurrentDate { get; }

    public void NotifyNextDay();
    public void AddDepositAccountPercent(StartAmountPercentPair depositAmountPercentPair);
    public CreditBankAccount CreateCreditAccount(BankClient client);
    public DebitBankAccount CreateDebitAccount(BankClient client);
    public DepositBankAccount CreateDepositAccount(BankClient client, MoneyAmount startMoneyAmount);
    public void DeleteAccount(IBankAccount account);
    public void DeleteAllClientAccounts(BankClient client);
    public void SubscribeToNotifications(BankClient client);
    public void UnsubscribeFromNotifications(BankClient client);
}