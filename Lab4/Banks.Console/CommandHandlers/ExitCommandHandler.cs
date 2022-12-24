using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.CommandHandlers;

public class ExitCommandHandler : CommandHandler
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