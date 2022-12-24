using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankCreationHandlers;

public class SetDepositAccountTermHandler : BankCreationCommandHandler
{
    public SetDepositAccountTermHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }

    public override IBank Handle()
    {
        InteractionInterface.WriteLine("input deposit account term:");
        CentralBank.SetDefaultDepositAccountTerm(UserInputParser.GetDepositTermDaysInput(InteractionInterface));
        return base.Handle();
    }
}