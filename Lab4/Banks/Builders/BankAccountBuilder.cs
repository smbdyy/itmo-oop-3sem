using Banks.Entities;
using Banks.Interfaces;
using Banks.Tools.Exceptions;

namespace Banks.Builders;

public abstract class BankAccountBuilder
{
    private IBank? _bank;
    private BankClient? _client;

    protected IBank Bank
    {
        get
        {
            if (_bank is null)
            {
                throw new RequiredFieldInBuilderIsNullException();
            }

            return _bank;
        }
    }

    protected BankClient Client
    {
        get
        {
            if (_client is null)
            {
                throw new RequiredFieldInBuilderIsNullException();
            }

            return _client;
        }
    }

    public abstract IBankAccount Build();

    public BankAccountBuilder SetBank(IBank bank)
    {
        _bank = bank;
        return this;
    }

    public BankAccountBuilder SetClient(BankClient client)
    {
        _client = client;
        return this;
    }
}