using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class ExitCommandHandler : MainMenuCommandHandler
{
    public ExitCommandHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }
    public override bool Handle(string command)
    {
        return command != "exit" && base.Handle(command);
    }
}