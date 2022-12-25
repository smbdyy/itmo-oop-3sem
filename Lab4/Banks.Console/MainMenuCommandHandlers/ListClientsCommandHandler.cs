using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class ListClientsCommandHandler : MainMenuCommandHandler
{
    private readonly ICentralBank _centralBank;

    public ListClientsCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(interactionInterface) => _centralBank = centralBank;

    public override void Handle(string command)
    {
        if (command == "list_c")
        {
            if (_centralBank.Clients.Count == 0)
            {
                InteractionInterface.WriteLine("no clients found");
                return;
            }

            var clients = _centralBank.Clients.ToList();
            for (int i = 0; i < clients.Count; i++)
            {
                InteractionInterface.WriteLine($"{i}. {clients[i].Name.AsString}, id {clients[i].Id}");
            }

            return;
        }

        base.Handle(command);
    }
}