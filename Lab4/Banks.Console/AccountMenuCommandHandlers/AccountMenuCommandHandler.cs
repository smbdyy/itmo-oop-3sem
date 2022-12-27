using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.AccountMenuCommandHandlers;

public abstract class AccountMenuCommandHandler
{
    private AccountMenuCommandHandler? _next;

    public AccountMenuCommandHandler(
        IUserInteractionInterface interactionInterface,
        AccountMenuContext context)
    {
        InteractionInterface = interactionInterface;
        Context = context;
    }

    protected IUserInteractionInterface InteractionInterface { get; }
    protected AccountMenuContext Context { get; }

    public AccountMenuCommandHandler SetNext(AccountMenuCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public AccountMenuCommandHandler SetAccount(IBankAccount account)
    {
        Context.SetAccount(account);
        return this;
    }

    public virtual bool Handle(string command)
    {
        if (_next is not null) return _next.Handle(command);

        InteractionInterface.WriteLine("unknown command");
        return true;
    }
}