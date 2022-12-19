using Banks.Builders;
using Banks.Entities;

namespace Banks.Interfaces;

public interface ICentralBank
{
    public DateOnly CurrentDate { get; }
    public BankBuilder BankBuilder { set; }

    public void SetDefaultDepositAccountTerm(int term);
    public void SetDefaultCreditAccountCommission(decimal commission);
    public void SetDefaultCreditAccountLimit(decimal limit);
    public void SetDefaultMaxUnverifiedClientWithdrawal(decimal value);
    public IBank CreateBank(string name);
    public void DeleteBank(IBank bank);
    public IBank? FindBankById(Guid id);
    public void RegisterClient(BankClient client);
    public void NotifyNextDay();
    public void DeleteClientAndAccounts(BankClient client);
}