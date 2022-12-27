using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.AccountMenuCommandHandlers;

public abstract class AccountMenuCommandHandler
{
    private AccountMenuCommandHandler? _next;

    public AccountMenuCommandHandler(IUserInteractionInterface interactionInterface)
        => InteractionInterface = interactionInterface;

    protected IUserInteractionInterface InteractionInterface { get; }
    protected IBankAccount? Account { get; private set; }

    public AccountMenuCommandHandler SetNext(AccountMenuCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public AccountMenuCommandHandler SetAccount(IBankAccount account)
    {
        Account = account;
        _next?.SetAccount(account);
        return this;
    }

    public virtual bool Handle(string command)
    {
        if (_next is not null) return _next.Handle(command);

        InteractionInterface.WriteLine("unknown command");
        return true;
    }
}