﻿using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankCreationHandlers;

public class SetUnverifiedClientWithdrawalLimitHandler : BankCreationCommandHandler
{
    public SetUnverifiedClientWithdrawalLimitHandler(
        ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }

    public override IBank Handle()
    {
        InteractionInterface.WriteLine("enter unverified client withdrawal limit");
        CentralBank.SetDefaultUnverifiedClientWithdrawalLimit(
            UserInputParser.GetUnsignedDecimal(InteractionInterface));
        return base.Handle();
    }
}