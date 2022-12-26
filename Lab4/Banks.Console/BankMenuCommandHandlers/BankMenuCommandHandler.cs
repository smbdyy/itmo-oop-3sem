using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public abstract class BankMenuCommandHandler
{
    private BankMenuCommandHandler? _next;

    public BankMenuCommandHandler(IUserInteractionInterface interactionInterface)
    {
        InteractionInterface = interactionInterface;
    }

    protected IUserInteractionInterface InteractionInterface { get; }

    public BankMenuCommandHandler SetNext(BankMenuCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public virtual bool Handle(string command)
    {
        if (_next is not null) return _next.Handle(command);

        InteractionInterface.WriteLine("unknown command");
        return true;
    }
}