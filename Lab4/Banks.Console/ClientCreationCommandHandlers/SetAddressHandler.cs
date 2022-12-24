using Banks.Builders;
using Banks.Console.AddressCreationCommandHandlers;
using Banks.Console.UserInteractionInterfaces;
using Banks.Entities;

namespace Banks.Console.ClientCreationCommandHandlers;

public class SetAddressHandler : ClientCreationCommandHandler
{
    public SetAddressHandler(BankClientBuilder builder, IUserInteractionInterface interactionInterface)
        : base(builder, interactionInterface) { }

    public override BankClient Handle()
    {
        InteractionInterface.WriteLine("do you want to enter the address? (y/n)");
        if (!UserInputParser.GetYesNoAnswerAsBool(InteractionInterface))
        {
            return base.Handle();
        }

        var builder = new AddressBuilder();
        AddressCreationCommandHandler creationHandlersChain = new SetCountryHandler(
            builder, InteractionInterface);

        creationHandlersChain
            .SetNext(new SetTownHandler(builder, InteractionInterface))
            .SetNext(new SetStreetHandler(builder, InteractionInterface))
            .SetNext(new SetHouseNumberHandler(builder, InteractionInterface));
        Builder.SetAddress(creationHandlersChain.Handle());

        return base.Handle();
    }
}