using Banks.Banks;
using Banks.Banks.Builders;
using Banks.CentralBanks;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankCreationHandlers;

public class SetNameHandler : BankCreationCommandHandler
{
    public SetNameHandler(
        ICentralBank centralBank,
        BankBuilder builder,
        IUserInteractionInterface interactionInterface)
        : base(centralBank, builder, interactionInterface) { }

    public override IBank Handle()
    {
        InteractionInterface.WriteLine("enter bank name:");
        Builder.SetName(UserInputParser.GetLine(InteractionInterface));
        return base.Handle();
    }
}
