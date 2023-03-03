using Banks.Console.UserInteractionInterfaces;
using Banks.Models;

namespace Banks.Console.BankMenuCommandHandlers;

public class BankInfoCommandHandler : BankMenuCommandHandler
{
    public BankInfoCommandHandler(IUserInteractionInterface interactionInterface, BankMenuContext context)
        : base(interactionInterface, context) { }

    public override bool Handle(string command)
    {
        if (command != "info") return base.Handle(command);

        System.Console.WriteLine($"Id: {Context.Bank.Id}");
        System.Console.WriteLine($"Name: {Context.Bank.Name}");
        System.Console.WriteLine($"Current date: {Context.Bank.CurrentDate}");
        System.Console.WriteLine($"Deposit account term: {Context.Bank.DepositAccountTerm.Value}");
        System.Console.WriteLine($"Credit commission: {Context.Bank.CreditAccountCommission.Value}");
        System.Console.WriteLine($"Credit account limit: {Context.Bank.CreditAccountLimit.Value}");
        System.Console.WriteLine($"Unverified client withdrawal limit: {Context.Bank.UnverifiedClientWithdrawalLimit.Value}");

        if (Context.Bank.DepositPercentInfo.Count == 0) return true;
        System.Console.WriteLine("Start amount -- percent pairs:");
        foreach (DepositPercentInfo pair in Context.Bank.DepositPercentInfo)
        {
            System.Console.WriteLine($"amount: {pair.StartAmount}, percent: {pair.Percent}");
        }

        return true;
    }
}