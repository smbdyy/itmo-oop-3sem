using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SetCommissionCommandHandler : BankMenuCommandHandler
{
    private readonly IBank _bank;

    public SetCommissionCommandHandler(IUserInteractionInterface interactionInterface, IBank bank)
        : base(interactionInterface) => _bank = bank;

    public override bool Handle(string command)
    {
        if (command != "set_cred_c") return base.Handle(command);

        InteractionInterface.WriteLine("enter new credit account commission:");
        _bank.CreditAccountCommission = UserInputParser.GetUnsignedDecimal(InteractionInterface);
        return true;
    }
}