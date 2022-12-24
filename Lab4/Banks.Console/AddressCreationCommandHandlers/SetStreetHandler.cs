using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Models;

namespace Banks.Console.AddressCreationCommandHandlers;

public class SetStreetHandler : AddressCreationCommandHandler
{
    public SetStreetHandler(AddressBuilder builder, IUserInteractionInterface interactionInterface)
        : base(builder, interactionInterface) { }

    public override Address Handle()
    {
        InteractionInterface.WriteLine("enter street:");
        Builder.SetStreet(UserInputParser.GetLine(InteractionInterface));
        return base.Handle();
    }
}