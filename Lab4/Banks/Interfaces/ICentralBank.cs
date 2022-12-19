using Banks.Builders;
using Banks.Entities;

namespace Banks.Interfaces;

public interface ICentralBank
{
    public DateOnly CurrentDate { get; }
    public BankBuilder BankBuilder { get; }

    public IBank CreateBank();
    public void RegisterClient(BankClient client);
    public void NotifyNextDay();
    public void DeleteClientAndAccounts(BankClient client);
}