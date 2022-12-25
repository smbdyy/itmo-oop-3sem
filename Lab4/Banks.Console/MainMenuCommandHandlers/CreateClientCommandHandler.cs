using Banks.Builders;
using Banks.Console.ClientCreationCommandHandlers;
using Banks.Console.UserInteractionInterfaces;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class CreateClientCommandHandler : MainMenuCommandHandler
{
    private readonly ClientCreationCommandHandler _clientCreationChain;

    public CreateClientCommandHandler(
        ICentralBank centralBank,
        IUserInteractionInterface interactionInterface,
        ClientCreationCommandHandler clientCreationChain)
        : base(centralBank, interactionInterface) => _clientCreationChain = clientCreationChain;

    public override void Handle(string command)
    {
        if (command == "create_c")
        {
            BankClient client = _clientCreationChain.Handle();
            CentralBank.RegisterClient(client);

            InteractionInterface.WriteLine($"client created, id: {client.Id}");
            return;
        }

        base.Handle(command);
    }
}