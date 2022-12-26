using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SetCommissionCommandHandler : BankMenuCommandHandler
{
    public SetCommissionCommandHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override bool Handle(string command)
    {
        if (command != "set_cred_c") return base.Handle(command);

        InteractionInterface.WriteLine("enter new credit account commission:");
        Bank!.CreditAccountCommission = UserInputParser.GetUnsignedDecimal(InteractionInterface);
        return true;
    }
}