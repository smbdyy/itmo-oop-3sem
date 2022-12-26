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

    public virtual void Handle(string command)
    {
        if (_next is null)
        {
            InteractionInterface.WriteLine("unknown command");
            return;
        }

        _next.Handle(command);
    }
}