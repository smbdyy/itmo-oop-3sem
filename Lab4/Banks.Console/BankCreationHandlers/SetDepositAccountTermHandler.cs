﻿using Banks.Banks;
using Banks.CentralBanks;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankCreationHandlers;

public class SetDepositAccountTermHandler : BankCreationCommandHandler
{
    public SetDepositAccountTermHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }

    public override IBank Handle()
    {
        InteractionInterface.WriteLine("enter deposit account term:");
        CentralBank.SetDefaultDepositAccountTerm(UserInputParser.GetUnsignedInt(InteractionInterface));
        return base.Handle();
    }
}