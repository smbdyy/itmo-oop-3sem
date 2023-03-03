using Banks.Banks;
using Banks.Banks.Builders;
using Banks.CentralBanks;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankCreationHandlers;

public class SetDepositAccountTermHandler : BankCreationCommandHandler
{
    public SetDepositAccountTermHandler(
        ICentralBank centralBank,
        BankBuilder builder,
        IUserInteractionInterface interactionInterface)
        : base(centralBank, builder, interactionInterface) { }

    public override IBank Handle()
    {
        InteractionInterface.WriteLine("enter deposit account term:");
        Builder.SetDepositAccountTerm(UserInputParser.GetUnsignedInt(InteractionInterface));
        return base.Handle();
    }
}