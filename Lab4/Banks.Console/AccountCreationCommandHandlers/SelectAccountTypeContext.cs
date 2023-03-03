using Banks.Banks;
using Banks.Console.Tools.Exception;

namespace Banks.Console.AccountCreationCommandHandlers;

public class SelectAccountTypeContext
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

    public SelectAccountTypeContext SetBank(IBank bank)
    {
        _bank = bank;
        return this;
    }

    public SelectAccountTypeContext SetAccountCreationChain(AccountCreationCommandHandler chain)
    {
        _accountCreationChain = chain;
        return this;
    }
}