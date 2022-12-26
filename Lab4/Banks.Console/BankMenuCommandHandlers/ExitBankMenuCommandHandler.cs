using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class ExitBankMenuCommandHandler : BankMenuCommandHandler
{
    public ExitBankMenuCommandHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override void Handle(string command)
    {
        if (command == "exit")
        {
            InteractionInterface.WriteLine("returning to main menu");
            return;
        }

        base.Handle(command);
    }
}