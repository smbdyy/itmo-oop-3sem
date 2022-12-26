using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public abstract class SelectAccountTypeCommandHandler
{
    private SelectAccountTypeCommandHandler? _next;

    public SelectAccountTypeCommandHandler(
        IUserInteractionInterface interactionInterface)
    {
        InteractionInterface = interactionInterface;
    }

    protected IUserInteractionInterface InteractionInterface { get; }
    protected AccountCreationCommandHandler? AccountCreationChain { get; private set; }
    protected IBank? Bank { get; private set; }

    public SelectAccountTypeCommandHandler SetNext(SelectAccountTypeCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public SelectAccountTypeCommandHandler SetAccountCreationChain(AccountCreationCommandHandler accountCreationChain)
    {
        AccountCreationChain = accountCreationChain;
        _next?.SetAccountCreationChain(accountCreationChain);
        return this;
    }

    public SelectAccountTypeCommandHandler SetBank(IBank bank)
    {
        Bank = bank;
        _next?.SetBank(bank);
        return this;
    }

    public virtual void Handle(string accountType)
    {
        if (_next is null)
        {
            InteractionInterface.WriteLine("unknown account type");
            return;
        }

        _next.Handle(accountType);
    }
}