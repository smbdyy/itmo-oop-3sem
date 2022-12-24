﻿using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankCreationHandlers;

public class SetCreditAccountCommissionHandler : BankCreationCommandHandler
{
    public SetCreditAccountCommissionHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }

    public override IBank Handle()
    {
        InteractionInterface.WriteLine("input credit account commission:");
        CentralBank.SetDefaultCreditAccountCommission(UserInputParser.GetMoneyAmountInput(InteractionInterface));
        return base.Handle();
    }
}