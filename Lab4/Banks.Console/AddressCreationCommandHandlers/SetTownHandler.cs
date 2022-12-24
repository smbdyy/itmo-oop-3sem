using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Models;

namespace Banks.Console.AddressCreationCommandHandlers;

public class SetTownHandler : AddressCreationCommandHandler
{
    public SetTownHandler(AddressBuilder builder, IUserInteractionInterface interactionInterface)
        : base(builder, interactionInterface) { }

    public override Address Handle()
    {
        InteractionInterface.WriteLine("enter town:");
        Builder.SetTown(UserInputParser.GetStringInput(InteractionInterface));
        return base.Handle();
    }
}