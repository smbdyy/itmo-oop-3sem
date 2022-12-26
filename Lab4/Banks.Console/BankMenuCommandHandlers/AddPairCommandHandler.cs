using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;
using Banks.Models;
using Banks.Tools.Exceptions;
using ArgumentException = System.ArgumentException;

namespace Banks.Console.BankMenuCommandHandlers;

public class AddPairCommandHandler : BankMenuCommandHandler
{
    private readonly IBank _bank;

    public AddPairCommandHandler(IUserInteractionInterface interactionInterface, IBank bank)
        : base(interactionInterface) => _bank = bank;

    public override void Handle(string command)
    {
        if (command == "add_pair")
        {
            System.Console.WriteLine("input start amount:");
            MoneyAmount amount = UserInputParser.GetUnsignedDecimal(InteractionInterface);
            System.Console.WriteLine("input percent:");
            MoneyAmount percent = UserInputParser.GetUnsignedDecimal(InteractionInterface);
            try
            {
                var pair = new StartAmountPercentPair(amount, percent);
                _bank.AddDepositAccountPercent(pair);
                System.Console.WriteLine("pair has been added");
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"incorrect input: {ex.Message}");
            }
            catch (AlreadyExistsException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }

            return;
        }

        base.Handle(command);
    }
}