using Banks.Accounts;
using Banks.Accounts.Builders;
using Banks.Clients;
using Banks.Models;

namespace Banks.Banks;

public interface IBank
{
    public Guid Id { get; }
    public string Name { get; }
    public IReadOnlyCollection<IBankAccount> Accounts { get; }
    public IReadOnlyCollection<DepositPercentInfo> DepositPercentInfo { get; }
    public IReadOnlyCollection<BankClient> Subscribers { get; }
    public DepositTermDays DepositAccountTerm { get; set; }
    public MoneyAmount CreditAccountCommission { get; set; }
    public NonPositiveMoneyAmount CreditAccountLimit { get; set; }
    public MoneyAmount UnverifiedClientWithdrawalLimit { get; set; }
    public DateOnly CurrentDate { get; }

    public void NotifyNextDay();
    public void AddDepositPercentInfo(DepositPercentInfo info);
    public void DeleteDepositPercentInfo(DepositPercentInfo info);
    public IBankAccount CreateAccount(BankAccountBuilder builder);
    public void DeleteAccount(IBankAccount account);
    public void DeleteAllClientAccounts(BankClient client);
    public void SubscribeToNotifications(BankClient client);
    public void UnsubscribeFromNotifications(BankClient client);
}