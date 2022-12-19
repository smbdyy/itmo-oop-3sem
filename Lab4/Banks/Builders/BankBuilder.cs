using Banks.Interfaces;

namespace Banks.Builders;

public abstract class BankBuilder
{
    protected string? Name { get; private set; } = null;
    protected int DepositAccountTerm { get; private set; } = 10;
    protected decimal CreditAccountCommission { get; private set; } = 10;
    protected decimal CreditAccountLimit { get; private set; } = -1000;
    protected decimal MaxUnverifiedClientWithdrawal { get; private set; } = 1000;
    public abstract IBank Build();
    public void Reset()
    {
        DepositAccountTerm = 10;
        CreditAccountCommission = 10;
        CreditAccountLimit = -1000;
        MaxUnverifiedClientWithdrawal = 1000;
    }

    public BankBuilder SetName(string name)
    {
        Name = name;
        return this;
    }

    public BankBuilder SetDepositAccountTerm(int term)
    {
        DepositAccountTerm = term;
        return this;
    }

    public BankBuilder SetCreditAccountCommission(decimal commission)
    {
        CreditAccountCommission = commission;
        return this;
    }

    public BankBuilder SetCreditAccountLimit(decimal limit)
    {
        CreditAccountLimit = limit;
        return this;
    }

    public BankBuilder SetMaxUnverifiedClientWithdrawal(decimal value)
    {
        MaxUnverifiedClientWithdrawal = value;
        return this;
    }
}