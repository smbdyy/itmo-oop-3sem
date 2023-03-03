using Banks.Banks;
using Banks.Banks.Builders;
using Banks.CentralBanks;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankCreationHandlers;

public class SetCreditAccountLimitHandler : BankCreationCommandHandler
{
    public SetCreditAccountLimitHandler(
        ICentralBank centralBank,
        BankBuilder builder,
        IUserInteractionInterface interactionInterface)
        : base(centralBank, builder, interactionInterface) { }

    public override IBank Handle()
    {
        InteractionInterface.WriteLine("enter credit account limit:");
        Builder.SetCreditAccountLimit(UserInputParser.GetNonPositiveDecimal(InteractionInterface));
        return base.Handle();
    }
}