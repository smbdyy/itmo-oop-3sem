using Banks.Builders;
using Banks.Entities;
using Banks.Models;

namespace Banks.Interfaces;

public interface ICentralBank
{
    public DateOnly CurrentDate { get; }
    public BankBuilder BankBuilder { set; }
    public IReadOnlyCollection<BankClient> Clients { get; }
    public IReadOnlyCollection<IBank> Banks { get; }

    public void SetDefaultDepositAccountTerm(DepositTermDays term);
    public void SetDefaultCreditAccountCommission(MoneyAmount commission);
    public void SetDefaultCreditAccountLimit(NonPositiveMoneyAmount limit);
    public void SetDefaultUnverifiedClientWithdrawalLimit(MoneyAmount value);
    public IBank CreateBank(string name);
    public void DeleteBank(IBank bank);
    public void RegisterClient(BankClient client);
    public void NotifyNextDay();
    public void DeleteClientAndAccounts(BankClient client);
}