using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.ClientMenuCommandHandlers;

public class ExitClientMenuCommandHandler : ClientMenuCommandHandler
{
    public ExitClientMenuCommandHandler(IUserInteractionInterface interactionInterface, ClientMenuContext context)
        : base(interactionInterface, context) { }

    public override bool Handle(string command)
    {
        if (command != "exit") return base.Handle(command);

        InteractionInterface.WriteLine("returning to main menu");
        return false;
    }
}