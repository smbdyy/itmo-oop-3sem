using Banks.Accounts.Builders;
using Banks.Banks;
using Banks.Console.Tools.Exception;

namespace Banks.Console.AccountCreationCommandHandlers;

public class AccountCreationContext
{
    private BankAccountBuilder? _builder;
    private IBank? _bank;

    public IBank Bank
    {
        get
        {
            if (_bank is null)
            {
                throw new ContextNotSetException();
            }

            return _bank;
        }
    }

    public BankAccountBuilder Builder
    {
        get
        {
            if (_builder is null)
            {
                throw new ContextNotSetException();
            }

            return _builder;
        }
    }

    public AccountCreationContext SetBank(IBank bank)
    {
        _bank = bank;
        return this;
    }

    public AccountCreationContext SetBuilder(BankAccountBuilder builder)
    {
        _builder = builder;
        return this;
    }
}