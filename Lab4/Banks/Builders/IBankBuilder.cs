using Banks.Interfaces;

namespace Banks.Builders;

public interface IBankBuilder
{
    public IBank Build();
    public void Reset();
    public IBankBuilder SetName(string name);
    public IBankBuilder SetDepositAccountTerm(int term);
    public IBankBuilder SetCreditAccountCommission(decimal commission);
    public IBankBuilder SetCreditAccountLimit(decimal limit);
    public IBankBuilder SetMaxUnverifiedClientWithdrawal(decimal value);
}