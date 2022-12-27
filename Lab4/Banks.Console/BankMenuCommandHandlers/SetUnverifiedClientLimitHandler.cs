using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SetUnverifiedClientLimitHandler : BankMenuCommandHandler
{
    public SetUnverifiedClientLimitHandler(IUserInteractionInterface interactionInterface, BankMenuContext context)
        : base(interactionInterface, context) { }

    public override bool Handle(string command)
    {
        if (command != "set_unv_l") return base.Handle(command);

        InteractionInterface.WriteLine("enter new unverified client withdrawal limit:");
        Context.Bank.UnverifiedClientWithdrawalLimit = UserInputParser.GetUnsignedDecimal(InteractionInterface);
        return true;
    }
}