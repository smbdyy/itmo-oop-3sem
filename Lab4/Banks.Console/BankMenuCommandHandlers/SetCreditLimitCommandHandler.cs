using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SetCreditLimitCommandHandler : BankMenuCommandHandler
{
    public SetCreditLimitCommandHandler(IUserInteractionInterface interactionInterface, BankMenuContext context)
        : base(interactionInterface, context) { }

    public override bool Handle(string command)
    {
        if (command != "set_cred_l") return base.Handle(command);

        InteractionInterface.WriteLine("enter new credit account limit:");
        Context.Bank.CreditAccountLimit = UserInputParser.GetNonPositiveDecimal(InteractionInterface);
        return true;
    }
}