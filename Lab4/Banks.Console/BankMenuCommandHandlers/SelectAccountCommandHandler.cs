using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SelectAccountCommandHandler : BankMenuCommandHandler
{
    public SelectAccountCommandHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override bool Handle(string command)
    {
        if (command != "select_acc") return base.Handle(command);

        if (Bank!.Accounts.Count == 0)
        {
            InteractionInterface.WriteLine("no accounts found");
            return true;
        }

        IBankAccount account = Utils.GetAccountByInputNumber(Bank, InteractionInterface);

        // TODO account menu
        return base.Handle(command);
    }
}