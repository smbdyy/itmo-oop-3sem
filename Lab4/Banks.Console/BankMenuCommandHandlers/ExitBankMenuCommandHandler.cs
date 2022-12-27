using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class ExitBankMenuCommandHandler : BankMenuCommandHandler
{
    public ExitBankMenuCommandHandler(IUserInteractionInterface interactionInterface, BankMenuContext context)
        : base(interactionInterface, context) { }

    public override bool Handle(string command)
    {
        if (command != "exit") return base.Handle(command);

        InteractionInterface.WriteLine("returning to main menu");
        return false;
    }
}