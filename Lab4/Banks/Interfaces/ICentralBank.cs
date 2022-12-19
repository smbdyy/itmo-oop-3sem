using Banks.Entities;

namespace Banks.Interfaces;

public interface ICentralBank
{
    public DateOnly CurrentDate { get; }

    public IBank CreateBank(
        string name,
        int depositAccountTerm,
        decimal creditAccountCommission,
        decimal creditAccountLimit,
        decimal maxUnverifiedClientWithdrawal);
    public void RegisterClient(BankClient client);
    public void NotifyNextDay();
    public void DeleteClientAndAccounts(BankClient client);
}