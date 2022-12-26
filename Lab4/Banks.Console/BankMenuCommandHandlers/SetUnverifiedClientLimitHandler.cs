using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SetUnverifiedClientLimitHandler : BankMenuCommandHandler
{
    private readonly IBank _bank;

    public SetUnverifiedClientLimitHandler(IUserInteractionInterface interactionInterface, IBank bank)
        : base(interactionInterface) => _bank = bank;

    public override bool Handle(string command)
    {
        if (command != "set_unv_l") return base.Handle(command);

        InteractionInterface.WriteLine("enter new unverified client withdrawal limit:");
        _bank.UnverifiedClientWithdrawalLimit = UserInputParser.GetUnsignedDecimal(InteractionInterface);
        return true;
    }
}