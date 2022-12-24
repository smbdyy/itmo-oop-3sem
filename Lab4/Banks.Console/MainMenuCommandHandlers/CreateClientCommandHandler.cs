using Banks.Builders;
using Banks.Console.ClientCreationCommandHandlers;
using Banks.Console.UserInteractionInterfaces;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class CreateClientCommandHandler : MainMenuCommandHandler
{
    public CreateClientCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }

    public override void Handle(string command)
    {
        if (command == "create_c")
        {
            var builder = new BankClientBuilder();
            ClientCreationCommandHandler creationHandlersChain =
                new SetClientNameHandler(builder, InteractionInterface);
            creationHandlersChain
                .SetNext(new SetAddressHandler(builder, InteractionInterface))
                .SetNext(new SetPassportNumberHandler(builder, InteractionInterface));

            BankClient client = creationHandlersChain.Handle();
            CentralBank.RegisterClient(client);

            InteractionInterface.WriteLine($"client created, id: {client.Id}");
            return;
        }

        base.Handle(command);
    }
}