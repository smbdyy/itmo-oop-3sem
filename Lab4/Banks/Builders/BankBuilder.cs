using Banks.Interfaces;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

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
        if (name == string.Empty)
        {
            throw ArgumentException.EmptyString();
        }

        Name = name;
        return this;
    }

    public BankBuilder SetDepositAccountTerm(int term)
    {
        DepositAccountTerm = ValidateNotNegative(term);
        return this;
    }

    public BankBuilder SetCreditAccountCommission(decimal commission)
    {
        CreditAccountCommission = ValidateNotNegative(commission);
        return this;
    }

    public BankBuilder SetCreditAccountLimit(decimal limit)
    {
        if (limit > 0)
        {
            throw ArgumentException.InappropriateNonNegativeNumber(limit);
        }

        CreditAccountLimit = limit;
        return this;
    }

    public BankBuilder SetMaxUnverifiedClientWithdrawal(decimal value)
    {
        MaxUnverifiedClientWithdrawal = ValidateNotNegative(value);
        return this;
    }

    private static decimal ValidateNotNegative(decimal value)
    {
        if (value < 0)
        {
            throw ArgumentException.InappropriateNegativeNumber(value);
        }

        return value;
    }

    private static int ValidateNotNegative(int value)
    {
        if (value < 0)
        {
            throw ArgumentException.InappropriateNegativeNumber(value);
        }

        return value;
    }
}