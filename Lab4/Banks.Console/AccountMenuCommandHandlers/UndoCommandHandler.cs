using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.AccountMenuCommandHandlers;

public class UndoCommandHandler : AccountMenuCommandHandler
{
    public UndoCommandHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override bool Handle(string command)
    {
        if (command != "undo") return base.Handle(command);

        var transactions = Account!.TransactionHistory.ToList();
        if (transactions.Count == 0)
        {
            InteractionInterface.WriteLine("no transactions found");
            return true;
        }

        new WriteHistoryCommandHandler(InteractionInterface).SetAccount(Account).Handle("hist");
        InteractionInterface.WriteLine("enter transaction number:");
        int number = UserInputParser.GetIntInRange(0, transactions.Count, InteractionInterface);
        ITransactionInfo transactionInfo = transactions[number];
        Account.Undo(transactionInfo.TransactionId);

        InteractionInterface.WriteLine("success");
        return true;
    }
}