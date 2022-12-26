using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SetCreditLimitCommandHandler : BankMenuCommandHandler
{
    private readonly IBank _bank;

    public SetCreditLimitCommandHandler(IUserInteractionInterface interactionInterface, IBank bank)
        : base(interactionInterface) => _bank = bank;

    public override void Handle(string command)
    {
        if (command == "set_cred_l")
        {
            InteractionInterface.WriteLine("enter new credit account limit:");
            _bank.CreditAccountLimit = UserInputParser.GetNonPositiveDecimal(InteractionInterface);
            return;
        }

        base.Handle(command);
    }
}