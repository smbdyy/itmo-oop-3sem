using Banks.Banks;
using Banks.Banks.Builders;
using Banks.CentralBanks;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankCreationHandlers;

public abstract class BankCreationCommandHandler
{
    private BankCreationCommandHandler? _next;

    public BankCreationCommandHandler(
        ICentralBank centralBank,
        BankBuilder builder,
        IUserInteractionInterface interactionInterface)
    {
        CentralBank = centralBank;
        Builder = builder;
        InteractionInterface = interactionInterface;
    }

    protected ICentralBank CentralBank { get; }
    protected BankBuilder Builder { get; }
    protected IUserInteractionInterface InteractionInterface { get; }

    public BankCreationCommandHandler SetNext(BankCreationCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public virtual IBank Handle()
    {
        return _next is null ? CentralBank.CreateBank(Builder) : _next.Handle();
    }
}