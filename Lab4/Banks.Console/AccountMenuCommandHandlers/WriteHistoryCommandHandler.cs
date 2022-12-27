using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.AccountMenuCommandHandlers;

public class WriteHistoryCommandHandler : AccountMenuCommandHandler
{
    public WriteHistoryCommandHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override bool Handle(string command)
    {
        if (command != "hist") return base.Handle(command);

        if (Account!.TransactionHistory.Count == 0)
        {
            InteractionInterface.WriteLine("no transactions found");
            return true;
        }

        var transactions = Account.TransactionHistory.ToList();
        for (int i = 0; i < transactions.Count; i++)
        {
            InteractionInterface.WriteLine($"{i}. {transactions[i].Description}");
        }

        return true;
    }
}