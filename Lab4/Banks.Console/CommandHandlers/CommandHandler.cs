using Banks.Console.UserInteractionInterfaces;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Console.CommandHandlers;

public abstract class CommandHandler
{
    private CommandHandler? _next;

    public CommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
    {
        CentralBank = centralBank;
        InteractionInterface = interactionInterface;
    }

    protected ICentralBank CentralBank { get; }
    protected IUserInteractionInterface InteractionInterface { get; }

    public virtual void Handle(string command)
    {
        if (_next is null)
        {
            InteractionInterface.WriteLine("unknown command");
            return;
        }

        _next.Handle(command);
    }

    public CommandHandler SetNext(CommandHandler nextHandler)
    {
        _next = nextHandler;
        return _next;
    }
}