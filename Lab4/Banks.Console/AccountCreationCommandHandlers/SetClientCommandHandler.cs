﻿using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public class SetClientCommandHandler : AccountCreationCommandHandler
{
    private readonly ICentralBank _centralBank;

    public SetClientCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(interactionInterface) => _centralBank = centralBank;

    public override void Handle()
    {
        InteractionInterface.WriteLine("select client:");
        BankClient client = Utils.GetClientByInputNumber(_centralBank, InteractionInterface);
        Builder!.SetClient(client);
        base.Handle();
    }
}