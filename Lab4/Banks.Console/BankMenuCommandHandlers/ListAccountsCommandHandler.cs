using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class ListAccountsCommandHandler : BankMenuCommandHandler
{
    public ListAccountsCommandHandler(IUserInteractionInterface interactionInterface, BankMenuContext context)
        : base(interactionInterface, context) { }

    public override bool Handle(string command)
    {
        if (command != "list_acc") return base.Handle(command);

        Utils.WriteAccountsList(Context.Bank, InteractionInterface);
        return true;
    }
}