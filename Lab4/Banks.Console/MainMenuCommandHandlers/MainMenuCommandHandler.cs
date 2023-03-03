using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public abstract class MainMenuCommandHandler
{
    private MainMenuCommandHandler? _next;

    public MainMenuCommandHandler(IUserInteractionInterface interactionInterface)
    {
        InteractionInterface = interactionInterface;
    }

    protected IUserInteractionInterface InteractionInterface { get; }

    public virtual bool Handle(string command)
    {
        if (_next is not null) return _next.Handle(command);

        InteractionInterface.WriteLine("unknown command");
        return true;
    }

    public MainMenuCommandHandler SetNext(MainMenuCommandHandler nextHandler)
    {
        _next = nextHandler;
        return _next;
    }
}