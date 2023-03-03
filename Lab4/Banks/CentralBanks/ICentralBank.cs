using Banks.Banks;
using Banks.Banks.Builders;
using Banks.Clients;

namespace Banks.CentralBanks;

public interface ICentralBank
{
    public DateOnly CurrentDate { get; }
    public IReadOnlyCollection<BankClient> Clients { get; }
    public IReadOnlyCollection<IBank> Banks { get; }
    public IBank CreateBank(BankBuilder builder);
    public void DeleteBank(IBank bank);
    public void RegisterClient(BankClient client);
    public void NotifyNextDay();
    public void DeleteClientAndAccounts(BankClient client);
}