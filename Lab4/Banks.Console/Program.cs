﻿using Banks.Builders;
using Banks.Console;
using Banks.Entities;
using Banks.Interfaces;

var centralBank = new CentralBank(new DefaultBankBuilder());
var centralBankInterface = new CentralBankConsoleInterface(centralBank);

Console.WriteLine(
    @"initialized, commands:
    exit
    create_b - create new bank
    select_b - select bank to manage
    create_c - create client
    select_c - select client to manage
");

while (true)
{
    string input = Utils.GetStringInput();
    switch (input)
    {
        case "exit":
            return;
        case "create_b":
            centralBankInterface.InputDepositAccountTerm();
            centralBankInterface.InputCreditAccountCommission();
            centralBankInterface.InputCreditAccountLimit();
            centralBankInterface.InputUnverifiedClientWithdrawalLimit();
            IBank bank = centralBankInterface.InputNameCreateBank();
            Console.WriteLine($"bank created, id: {bank.Id}");
            break;

        case "create_c":
            var builder = new ConsoleClientBuilder(centralBank, new BankClientBuilder());
            builder.InputName();
            builder.InputAddress();
            builder.InputPassportNumber();
            BankClient client = builder.Build();
            Console.WriteLine($"client created, id: {client.Id}");
            break;

        default:
            Console.WriteLine("incorrect input");
            break;
    }
}
