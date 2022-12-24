using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public abstract class MainMenuCommandHandler
{
    private MainMenuCommandHandler? _next;

    public MainMenuCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
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

    public MainMenuCommandHandler SetNext(MainMenuCommandHandler nextHandler)
    {
        _next = nextHandler;
        return _next;
    }
}