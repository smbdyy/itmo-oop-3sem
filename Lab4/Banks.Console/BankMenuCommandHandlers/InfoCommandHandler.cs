using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Console.BankMenuCommandHandlers;

public class InfoCommandHandler : BankMenuCommandHandler
{
    private readonly IBank _bank;

    public InfoCommandHandler(IUserInteractionInterface interactionInterface, IBank bank)
        : base(interactionInterface) => _bank = bank;

    public override bool Handle(string command)
    {
        if (command != "info") return base.Handle(command);

        System.Console.WriteLine($"Id: {_bank.Id}");
        System.Console.WriteLine($"Name: {_bank.Name}");
        System.Console.WriteLine($"Current date: {_bank.CurrentDate}");
        System.Console.WriteLine($"Deposit account term: {_bank.DepositAccountTerm}");
        System.Console.WriteLine($"Credit commission: {_bank.CreditAccountCommission}");
        System.Console.WriteLine($"Credit account limit: {_bank.CreditAccountLimit}");
        System.Console.WriteLine($"Unverified client withdrawal limit: {_bank.UnverifiedClientWithdrawalLimit}");

        if (_bank.StartAmountPercentPairs.Count == 0) return true;
        System.Console.WriteLine("Start amount -- percent pairs:");
        foreach (StartAmountPercentPair pair in _bank.StartAmountPercentPairs)
        {
            System.Console.WriteLine($"amount: {pair.StartAmount}, percent: {pair.Percent}");
        }

        return true;
    }
}