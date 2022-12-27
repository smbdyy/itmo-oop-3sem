﻿using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public abstract class AccountCreationCommandHandler
{
    private AccountCreationCommandHandler? _next;

    public AccountCreationCommandHandler(
        IUserInteractionInterface interactionInterface,
        AccountCreationContext context)
    {
        InteractionInterface = interactionInterface;
        Context = context;
    }

    public AccountCreationCommandHandler(IUserInteractionInterface interactionInterface)
        : this(interactionInterface, new AccountCreationContext()) { }

    protected IUserInteractionInterface InteractionInterface { get; }
    protected AccountCreationContext Context { get; }

    public AccountCreationCommandHandler SetNext(AccountCreationCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public AccountCreationCommandHandler SetBuilder(BankAccountBuilder builder)
    {
        Context.SetBuilder(builder);
        return this;
    }

    public AccountCreationCommandHandler SetBank(IBank bank)
    {
        Context.SetBank(bank);
        return this;
    }

    public virtual void Handle()
    {
        if (_next is null)
        {
            IBankAccount account = Context.Bank.CreateAccount(Context.Builder);
            InteractionInterface.WriteLine($"account created, id: {account.Id}");
            return;
        }

        _next.Handle();
    }
}