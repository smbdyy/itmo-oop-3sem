using Banks.Entities;
using Banks.Interfaces;
using Banks.Tools.Exceptions;

namespace Banks.Builders;

public abstract class BankAccountBuilder
{
    protected IBank? Bank { get; private set; }
    protected BankClient? Client { get; private set; }

    public abstract IBankAccount Build();

    public BankAccountBuilder SetBank(IBank bank)
    {
        Bank = bank;
        return this;
    }

    public BankAccountBuilder SetClient(BankClient client)
    {
        Client = client;
        return this;
    }

    protected void ValidateNotNull()
    {
        if (Bank is null || Client is null)
        {
            throw new RequiredFieldInBuilderIsNullException();
        }
    }
}