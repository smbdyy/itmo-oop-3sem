using Banks.Banks;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public abstract class SelectAccountTypeCommandHandler
{
    private SelectAccountTypeCommandHandler? _next;

    public SelectAccountTypeCommandHandler(
        IUserInteractionInterface interactionInterface,
        SelectAccountTypeContext context)
    {
        InteractionInterface = interactionInterface;
        Context = context;
    }

    protected IUserInteractionInterface InteractionInterface { get; }
    protected SelectAccountTypeContext Context { get; }

    public SelectAccountTypeCommandHandler SetNext(SelectAccountTypeCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public SelectAccountTypeCommandHandler SetAccountCreationChain(AccountCreationCommandHandler accountCreationChain)
    {
        Context.SetAccountCreationChain(accountCreationChain);
        return this;
    }

    public SelectAccountTypeCommandHandler SetBank(IBank bank)
    {
        Context.SetBank(bank);
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