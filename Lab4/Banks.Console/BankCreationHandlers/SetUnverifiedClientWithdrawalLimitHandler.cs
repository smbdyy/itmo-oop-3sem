using Banks.Banks;
using Banks.Banks.Builders;
using Banks.CentralBanks;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankCreationHandlers;

public class SetUnverifiedClientWithdrawalLimitHandler : BankCreationCommandHandler
{
    public SetUnverifiedClientWithdrawalLimitHandler(
        ICentralBank centralBank,
        BankBuilder builder,
        IUserInteractionInterface interactionInterface)
        : base(centralBank, builder, interactionInterface) { }

    public override IBank Handle()
    {
        InteractionInterface.WriteLine("enter unverified client withdrawal limit");
        Builder.SetUnverifiedClientWithdrawalLimit(
            UserInputParser.GetUnsignedDecimal(InteractionInterface));
        return base.Handle();
    }
}