using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class ListClientsCommandHandler : MainMenuCommandHandler
{
    public ListClientsCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }

    public override void Handle(string command)
    {
        if (command == "list_c")
        {
            if (CentralBank.Clients.Count == 0)
            {
                InteractionInterface.WriteLine("no clients found");
                return;
            }

            var clients = CentralBank.Clients.ToList();
            for (int i = 0; i < clients.Count; i++)
            {
                InteractionInterface.WriteLine($"{i}. {clients[i].Name.AsString}, id {clients[i].Id}");
            }

            return;
        }

        base.Handle(command);
    }
}