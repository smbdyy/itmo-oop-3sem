using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SetCommissionCommandHandler : BankMenuCommandHandler
{
    public SetCommissionCommandHandler(IUserInteractionInterface interactionInterface, BankMenuContext context)
        : base(interactionInterface, context) { }

    public override bool Handle(string command)
    {
        if (command != "set_cred_c") return base.Handle(command);

        InteractionInterface.WriteLine("enter new credit account commission:");
        Context.Bank.CreditAccountCommission = UserInputParser.GetUnsignedDecimal(InteractionInterface);
        return true;
    }
}