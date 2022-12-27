using Banks.Console.UserInteractionInterfaces;
using Banks.Models;
using Banks.Tools.Exceptions;

namespace Banks.Console.AccountMenuCommandHandlers;

public class WithdrawCommandHandler : AccountMenuCommandHandler
{
    public WithdrawCommandHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override bool Handle(string command)
    {
        if (command != "with") return base.Handle(command);

        InteractionInterface.WriteLine("enter money amount:");
        MoneyAmount amount = UserInputParser.GetUnsignedDecimal(InteractionInterface);
        try
        {
            Account!.Withdraw(amount);
            InteractionInterface.WriteLine("success");
        }
        catch (TransactionValidationException ex)
        {
            InteractionInterface.WriteLine($"transaction failed: {ex.Message}");
        }

        return true;
    }
}