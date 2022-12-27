using Banks.Console.Tools.Exception;
using Banks.Interfaces;

namespace Banks.Console.AccountMenuCommandHandlers;

public class AccountMenuContext
{
    private IBankAccount? _account;

    public IBankAccount Account
    {
        get
        {
            if (_account is null)
            {
                throw new ContextNotSetException();
            }

            return _account;
        }
    }

    public AccountMenuContext SetAccount(IBankAccount account)
    {
        _account = account;
        return this;
    }
}