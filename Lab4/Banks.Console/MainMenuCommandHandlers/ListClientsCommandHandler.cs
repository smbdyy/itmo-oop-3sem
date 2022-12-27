using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class ListClientsCommandHandler : MainMenuCommandHandler
{
    private readonly ICentralBank _centralBank;

    public ListClientsCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(interactionInterface) => _centralBank = centralBank;

    public override bool Handle(string command)
    {
        if (command != "list_c") return base.Handle(command);

        Utils.WriteClientsList(_centralBank, InteractionInterface);
        return true;
    }
}