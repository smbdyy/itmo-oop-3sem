using Banks.Console.Tools.Exception;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class BankMenuContext
{
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

    public BankMenuContext SetBank(IBank bank)
    {
        _bank = bank;
        return this;
    }
}