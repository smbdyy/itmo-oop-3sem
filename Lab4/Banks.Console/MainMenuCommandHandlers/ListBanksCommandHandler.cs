using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class ListBanksCommandHandler : MainMenuCommandHandler
{
    private readonly ICentralBank _centralBank;

    public ListBanksCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(interactionInterface) => _centralBank = centralBank;

    public override void Handle(string command)
    {
        if (command == "list_b")
        {
            Utils.WriteBanksList(_centralBank, InteractionInterface);
            return;
        }

        base.Handle(command);
    }
}