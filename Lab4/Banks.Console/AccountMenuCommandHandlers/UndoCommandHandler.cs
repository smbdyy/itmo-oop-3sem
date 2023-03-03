using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Transactions.Info;

namespace Banks.Console.AccountMenuCommandHandlers;

public class UndoCommandHandler : AccountMenuCommandHandler
{
    public UndoCommandHandler(
        IUserInteractionInterface interactionInterface,
        AccountMenuContext context)
        : base(interactionInterface, context) { }

    public override bool Handle(string command)
    {
        if (command != "undo") return base.Handle(command);

        var transactions = Context.Account.TransactionHistory.ToList();
        if (transactions.Count == 0)
        {
            InteractionInterface.WriteLine("no transactions found");
            return true;
        }

        new WriteHistoryCommandHandler(InteractionInterface, Context).SetAccount(Context.Account).Handle("hist");
        InteractionInterface.WriteLine("enter transaction number:");
        int number = UserInputParser.GetIntInRange(0, transactions.Count, InteractionInterface);
        ITransactionInfo transactionInfo = transactions[number];
        Context.Account.Undo(transactionInfo.TransactionId);

        InteractionInterface.WriteLine("success");
        return true;
    }
}