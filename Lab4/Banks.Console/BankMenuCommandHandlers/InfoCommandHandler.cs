﻿using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Console.BankMenuCommandHandlers;

public class InfoCommandHandler : BankMenuCommandHandler
{
    public InfoCommandHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override bool Handle(string command)
    {
        if (command != "info") return base.Handle(command);

        System.Console.WriteLine($"Id: {Bank!.Id}");
        System.Console.WriteLine($"Name: {Bank.Name}");
        System.Console.WriteLine($"Current date: {Bank.CurrentDate}");
        System.Console.WriteLine($"Deposit account term: {Bank.DepositAccountTerm}");
        System.Console.WriteLine($"Credit commission: {Bank.CreditAccountCommission}");
        System.Console.WriteLine($"Credit account limit: {Bank.CreditAccountLimit}");
        System.Console.WriteLine($"Unverified client withdrawal limit: {Bank.UnverifiedClientWithdrawalLimit}");

        if (Bank.StartAmountPercentPairs.Count == 0) return true;
        System.Console.WriteLine("Start amount -- percent pairs:");
        foreach (StartAmountPercentPair pair in Bank.StartAmountPercentPairs)
        {
            System.Console.WriteLine($"amount: {pair.StartAmount}, percent: {pair.Percent}");
        }

        return true;
    }
}