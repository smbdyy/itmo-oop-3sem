using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SelectAccountCommandHandler : BankMenuCommandHandler
{
    private readonly IBank _bank;

    public SelectAccountCommandHandler(IUserInteractionInterface interactionInterface, IBank bank)
        : base(interactionInterface) => _bank = bank;

    public override bool Handle(string command)
    {
        if (command != "select_acc") return base.Handle(command);

        if (_bank.Accounts.Count == 0)
        {
            InteractionInterface.WriteLine("no accounts found");
            return true;
        }

        IBankAccount account = Utils.GetAccountByInputNumber(_bank, InteractionInterface);

        // TODO account menu
        return base.Handle(command);
    }
}