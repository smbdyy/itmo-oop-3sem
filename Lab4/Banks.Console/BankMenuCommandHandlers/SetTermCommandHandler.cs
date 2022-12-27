using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SetTermCommandHandler : BankMenuCommandHandler
{
    public SetTermCommandHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override bool Handle(string command)
    {
        if (command != "set_term") return base.Handle(command);

        InteractionInterface.WriteLine("enter new deposit account term:");
        Bank!.DepositAccountTerm = UserInputParser.GetUnsignedInt(InteractionInterface);
        return true;
    }
}