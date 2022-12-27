using Banks.Console.Tools.Exception;
using Banks.Interfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public class AccountCreationContext
{
    private IBank? _bank;
    private AccountCreationCommandHandler? _accountCreationChain;

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

    public AccountCreationCommandHandler AccountCreationChain
    {
        get
        {
            if (_accountCreationChain is null)
            {
                throw new ContextNotSetException();
            }

            return _accountCreationChain;
        }
    }

    public AccountCreationContext SetBank(IBank bank)
    {
        _bank = bank;
        return this;
    }

    public AccountCreationContext SetAccountCreationChain(AccountCreationCommandHandler chain)
    {
        _accountCreationChain = chain;
        return this;
    }
}