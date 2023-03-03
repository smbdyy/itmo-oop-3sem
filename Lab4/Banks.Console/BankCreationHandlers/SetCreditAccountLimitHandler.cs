using Banks.Banks;
using Banks.CentralBanks;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankCreationHandlers;

public class SetCreditAccountLimitHandler : BankCreationCommandHandler
{
    public SetCreditAccountLimitHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }

    public override IBank Handle()
    {
        InteractionInterface.WriteLine("enter credit account limit:");
        CentralBank.SetDefaultCreditAccountLimit(UserInputParser.GetNonPositiveDecimal(InteractionInterface));
        return base.Handle();
    }
}