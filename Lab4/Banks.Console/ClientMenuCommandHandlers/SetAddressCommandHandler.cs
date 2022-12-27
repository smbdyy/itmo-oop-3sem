using Banks.Console.AddressCreationCommandHandlers;
using Banks.Console.UserInteractionInterfaces;
using Banks.Models;

namespace Banks.Console.ClientMenuCommandHandlers;

public class SetAddressCommandHandler : ClientMenuCommandHandler
{
    private readonly AddressCreationCommandHandler _addressCreationChain;

    public SetAddressCommandHandler(
        IUserInteractionInterface interactionInterface,
        AddressCreationCommandHandler addressCreationChain)
        : base(interactionInterface) => _addressCreationChain = addressCreationChain;

    public override bool Handle(string command)
    {
        if (command != "set_address") return base.Handle(command);

        Address address = _addressCreationChain.Handle();
        Client!.Address = address;
        InteractionInterface.WriteLine("success");
        return true;
    }
}