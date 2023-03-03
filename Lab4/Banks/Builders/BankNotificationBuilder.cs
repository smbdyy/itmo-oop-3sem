using Banks.Banks;

namespace Banks.Builders;

public abstract class BankNotificationBuilder
{
    protected IBank? Bank { get; private set; }

    public BankNotificationBuilder SetBank(IBank bank)
    {
        Bank = bank;
        return this;
    }

    public abstract string GetNotificationMessage();
}