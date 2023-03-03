using Banks.CentralBanks;
using Banks.Clients;
using Banks.Console.ClientCreationCommandHandlers;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class CreateClientCommandHandler : MainMenuCommandHandler
{
    private readonly ClientCreationCommandHandler _clientCreationChain;
    private readonly ICentralBank _centralBank;

    public CreateClientCommandHandler(
        ICentralBank centralBank,
        IUserInteractionInterface interactionInterface,
        ClientCreationCommandHandler clientCreationChain)
        : base(interactionInterface)
    {
        _clientCreationChain = clientCreationChain;
        _centralBank = centralBank;
    }

    public override bool Handle(string command)
    {
        if (command != "create_c") return base.Handle(command);

        BankClient client = _clientCreationChain.Handle();
        _centralBank.RegisterClient(client);
        InteractionInterface.WriteLine($"client created, id: {client.Id}");
        return true;
    }
}