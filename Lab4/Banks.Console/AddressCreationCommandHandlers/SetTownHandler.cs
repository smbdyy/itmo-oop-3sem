using Banks.Builders;
using Banks.Console.Tools;
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
        Builder.SetTown(UserInputParser.GetLine(InteractionInterface));
        return base.Handle();
    }
}