using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class ListBanksCommandHandler : MainMenuCommandHandler
{
    private readonly ICentralBank _centralBank;

    public ListBanksCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(interactionInterface) => _centralBank = centralBank;

    public override bool Handle(string command)
    {
        if (command != "list_b") return base.Handle(command);

        Utils.WriteBanksList(_centralBank, InteractionInterface);
        return true;
    }
}