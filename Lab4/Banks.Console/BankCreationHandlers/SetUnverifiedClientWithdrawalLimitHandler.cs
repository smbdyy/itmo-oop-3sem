using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankCreationHandlers;

public class SetUnverifiedClientWithdrawalLimitHandler : BankCreationCommandHandler
{
    public SetUnverifiedClientWithdrawalLimitHandler(
        ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }

    public override IBank Handle()
    {
        InteractionInterface.WriteLine("input unverified client withdrawal limit");
        CentralBank.SetDefaultUnverifiedClientWithdrawalLimit(
            UserInputParser.GetMoneyAmountInput(InteractionInterface));
        return base.Handle();
    }
}