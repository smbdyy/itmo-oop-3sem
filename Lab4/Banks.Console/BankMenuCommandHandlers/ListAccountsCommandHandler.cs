using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class ListAccountsCommandHandler : BankMenuCommandHandler
{
    private readonly IBank _bank;

    public ListAccountsCommandHandler(IUserInteractionInterface interactionInterface, IBank bank)
        : base(interactionInterface) => _bank = bank;

    public override void Handle(string command)
    {
        if (command == "list_acc")
        {
            Utils.WriteAccountsList(_bank, InteractionInterface);
            return;
        }

        base.Handle(command);
    }
}