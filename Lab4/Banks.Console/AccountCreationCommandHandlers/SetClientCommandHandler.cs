using Banks.Builders;
using Banks.Console.MainMenuCommandHandlers;
using Banks.Console.UserInteractionInterfaces;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public class SetClientCommandHandler : AccountCreationCommandHandler
{
    public SetClientCommandHandler(
        BankAccountBuilder builder, ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(builder, centralBank, interactionInterface) { }

    public override IBankAccount Handle()
    {
        InteractionInterface.WriteLine("select client:");
        new ListClientsCommandHandler(CentralBank, InteractionInterface).Handle("list_c");
        InteractionInterface.WriteLine("enter number:");

        var clients = CentralBank.Clients.ToList();
        BankClient client = clients[UserInputParser.GetIntInRange(0, clients.Count, InteractionInterface)];
        Builder.SetClient(client);

        return base.Handle();
    }
}