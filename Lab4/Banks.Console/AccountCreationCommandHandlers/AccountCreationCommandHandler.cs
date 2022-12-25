using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public abstract class AccountCreationCommandHandler
{
    private AccountCreationCommandHandler? _next;

    public AccountCreationCommandHandler(
        BankAccountBuilder builder, IUserInteractionInterface interactionInterface)
    {
        Builder = builder;
        InteractionInterface = interactionInterface;
    }

    protected BankAccountBuilder Builder { get; }
    protected IUserInteractionInterface InteractionInterface { get; }

    public AccountCreationCommandHandler SetNext(AccountCreationCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public virtual IBankAccount Handle()
    {
        return _next is null ? Builder.Build() : _next.Handle();
    }
}