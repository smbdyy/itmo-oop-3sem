﻿using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankCreationHandlers;

public class SetNameHandler : BankCreationCommandHandler
{
    public SetNameHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }

    public override IBank Handle()
    {
        InteractionInterface.WriteLine("enter bank name:");
        CentralBank.SetNewBankName(UserInputParser.GetLine(InteractionInterface));
        return base.Handle();
    }
}