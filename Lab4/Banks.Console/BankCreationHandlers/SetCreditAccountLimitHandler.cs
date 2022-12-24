using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankCreationHandlers;

public class SetCreditAccountLimitHandler : BankCreationCommandHandler
{
    public SetCreditAccountLimitHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }

    public override IBank Handle()
    {
        InteractionInterface.WriteLine("enter credit account limit:");
        CentralBank.SetDefaultCreditAccountLimit(UserInputParser.GetNonPositiveMoneyAmount(InteractionInterface));
        return base.Handle();
    }
}