using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class ExitCommandHandler : MainMenuCommandHandler
{
    public ExitCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }
    public override void Handle(string command)
    {
        if (command == "exit")
        {
            return;
        }

        base.Handle(command);
    }
}