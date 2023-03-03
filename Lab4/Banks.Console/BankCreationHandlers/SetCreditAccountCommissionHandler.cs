using Banks.Banks;
using Banks.CentralBanks;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankCreationHandlers;

public class SetCreditAccountCommissionHandler : BankCreationCommandHandler
{
    public SetCreditAccountCommissionHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }

    public override IBank Handle()
    {
        InteractionInterface.WriteLine("enter credit account commission:");
        CentralBank.SetDefaultCreditAccountCommission(UserInputParser.GetUnsignedDecimal(InteractionInterface));
        return base.Handle();
    }
}