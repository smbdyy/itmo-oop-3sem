using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Models;
using Banks.Tools.Exceptions;

namespace Banks.Console.AccountMenuCommandHandlers;

public class ReplenishCommandHandler : AccountMenuCommandHandler
{
    public ReplenishCommandHandler(
        IUserInteractionInterface interactionInterface,
        AccountMenuContext context)
        : base(interactionInterface, context) { }

    public override bool Handle(string command)
    {
        if (command != "repl") return base.Handle(command);

        InteractionInterface.WriteLine("enter amount:");
        MoneyAmount amount = UserInputParser.GetUnsignedDecimal(InteractionInterface);
        try
        {
            Context.Account.Replenish(amount);
            InteractionInterface.WriteLine("success");
        }
        catch (TransactionValidationException ex)
        {
            InteractionInterface.WriteLine($"transaction failed: {ex.Message}");
        }

        return true;
    }
}