using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Models;

namespace Banks.Console.AddressCreationCommandHandlers;

public class SetHouseNumberHandler : AddressCreationCommandHandler
{
    public SetHouseNumberHandler(AddressBuilder builder, IUserInteractionInterface interactionInterface)
        : base(builder, interactionInterface) { }

    public override Address Handle()
    {
        InteractionInterface.WriteLine("enter house number:");
        Builder.SetHouseNumber(UserInputParser.GetLine(InteractionInterface));
        return base.Handle();
    }
}