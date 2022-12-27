using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;
using Banks.Models;
using Banks.Tools.Exceptions;
using ArgumentException = System.ArgumentException;

namespace Banks.Console.BankMenuCommandHandlers;

public class AddPairCommandHandler : BankMenuCommandHandler
{
    public AddPairCommandHandler(IUserInteractionInterface interactionInterface, BankMenuContext context)
        : base(interactionInterface, context) { }

    public override bool Handle(string command)
    {
        if (command != "add_pair") return base.Handle(command);

        System.Console.WriteLine("input start amount:");
        MoneyAmount amount = UserInputParser.GetUnsignedDecimal(InteractionInterface);
        System.Console.WriteLine("input percent:");
        MoneyAmount percent = UserInputParser.GetUnsignedDecimal(InteractionInterface);
        try
        {
            var pair = new StartAmountPercentPair(amount, percent);
            Context.Bank.AddDepositAccountPercent(pair);
            System.Console.WriteLine("pair has been added");
            return true;
        }
        catch (ArgumentException ex)
        {
            System.Console.WriteLine($"incorrect input: {ex.Message}");
        }
        catch (AlreadyExistsException ex)
        {
            System.Console.WriteLine($"exception: {ex.Message}");
        }

        return true;
    }
}