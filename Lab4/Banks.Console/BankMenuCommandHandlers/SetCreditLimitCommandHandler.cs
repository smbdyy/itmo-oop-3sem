using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SetCreditLimitCommandHandler : BankMenuCommandHandler
{
    public SetCreditLimitCommandHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override bool Handle(string command)
    {
        if (command != "set_cred_l") return base.Handle(command);

        InteractionInterface.WriteLine("enter new credit account limit:");
        Bank!.CreditAccountLimit = UserInputParser.GetNonPositiveDecimal(InteractionInterface);
        return true;
    }
}