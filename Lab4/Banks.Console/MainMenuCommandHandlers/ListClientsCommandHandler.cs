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
            Utils.WriteClientsList(_centralBank, InteractionInterface);
            return;
        }

        base.Handle(command);
    }
}