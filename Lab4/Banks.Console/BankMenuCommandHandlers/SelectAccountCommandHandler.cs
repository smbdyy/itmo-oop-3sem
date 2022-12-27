using Banks.Console.AccountMenuCommandHandlers;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SelectAccountCommandHandler : BankMenuCommandHandler
{
    private readonly AccountMenuCommandHandler _accountMenuChain;

    public SelectAccountCommandHandler(
        IUserInteractionInterface interactionInterface,
        AccountMenuCommandHandler accountMenuChain,
        BankMenuContext context)
        : base(interactionInterface, context) => _accountMenuChain = accountMenuChain;

    public override bool Handle(string command)
    {
        if (command != "select_acc") return base.Handle(command);

        if (Context.Bank.Accounts.Count == 0)
        {
            InteractionInterface.WriteLine("no accounts found");
            return true;
        }

        IBankAccount account = Utils.GetAccountByInputNumber(Context.Bank, InteractionInterface);
        _accountMenuChain.SetAccount(account);
        InteractionInterface.WriteLine($"managing account id {account.Id}");
        while (_accountMenuChain.Handle(UserInputParser.GetLine(InteractionInterface)))
        {
        }

        return base.Handle(command);
    }
}