using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Models;

namespace Banks.Console.AddressCreationCommandHandlers;

public class SetCountryHandler : AddressCreationCommandHandler
{
    public SetCountryHandler(AddressBuilder builder, IUserInteractionInterface interactionInterface)
        : base(builder, interactionInterface) { }

    public override Address Handle()
    {
        InteractionInterface.WriteLine("enter country:");
        Builder.SetCountry(UserInputParser.GetLine(InteractionInterface));
        return base.Handle();
    }
}