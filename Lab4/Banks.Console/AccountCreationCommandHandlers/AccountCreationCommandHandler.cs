using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public abstract class AccountCreationCommandHandler
{
    private AccountCreationCommandHandler? _next;

    public AccountCreationCommandHandler(IUserInteractionInterface interactionInterface)
    {
        InteractionInterface = interactionInterface;
    }

    protected BankAccountBuilder? Builder { get; private set; }
    protected IUserInteractionInterface InteractionInterface { get; }
    protected IBank? Bank { get; private set; }

    public AccountCreationCommandHandler SetNext(AccountCreationCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public AccountCreationCommandHandler SetBuilder(BankAccountBuilder builder)
    {
        Builder = builder;
        _next?.SetBuilder(builder);
        return this;
    }

    public AccountCreationCommandHandler SetBank(IBank bank)
    {
        Bank = bank;
        _next?.SetBank(bank);
        return this;
    }

    public virtual void Handle()
    {
        if (_next is null)
        {
            IBankAccount account = Bank!.CreateAccount(Builder!);
            InteractionInterface.WriteLine($"account created, id: {account.Id}");
            return;
        }

        _next.Handle();
    }
}