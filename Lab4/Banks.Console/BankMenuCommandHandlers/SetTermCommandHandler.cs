﻿using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SetTermCommandHandler : BankMenuCommandHandler
{
    private readonly IBank _bank;

    public SetTermCommandHandler(IUserInteractionInterface interactionInterface, IBank bank)
        : base(interactionInterface) => _bank = bank;

    public override void Handle(string command)
    {
        if (command == "set_term")
        {
            InteractionInterface.WriteLine("enter new deposit account term:");
            _bank.DepositAccountTerm = UserInputParser.GetUnsignedInt(InteractionInterface);
            return;
        }

        base.Handle(command);
    }
}