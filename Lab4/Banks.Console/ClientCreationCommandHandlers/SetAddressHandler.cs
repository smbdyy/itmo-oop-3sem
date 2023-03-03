using Banks.Clients;
using Banks.Console.AddressCreationCommandHandlers;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.ClientCreationCommandHandlers;

public class SetAddressHandler : ClientCreationCommandHandler
{
    private readonly AddressCreationCommandHandler _addressCreationChain;

    public SetAddressHandler(
        BankClientBuilder builder,
        IUserInteractionInterface interactionInterface,
        AddressCreationCommandHandler addressCreationChain)
        : base(builder, interactionInterface) => _addressCreationChain = addressCreationChain;

    public override BankClient Handle()
    {
        InteractionInterface.WriteLine("do you want to enter the address? (y/n)");
        if (!UserInputParser.GetYesNoAnswerAsBool(InteractionInterface))
        {
            return base.Handle();
        }

        Builder.SetAddress(_addressCreationChain.Handle());

        return base.Handle();
    }
}