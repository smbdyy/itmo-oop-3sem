using Banks.Banks;
using Banks.CentralBanks;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankCreationHandlers;

public abstract class BankCreationCommandHandler
{
    private BankCreationCommandHandler? _next;

    public BankCreationCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
    {
        CentralBank = centralBank;
        InteractionInterface = interactionInterface;
    }

    protected ICentralBank CentralBank { get; }
    protected IUserInteractionInterface InteractionInterface { get; }

    public BankCreationCommandHandler SetNext(BankCreationCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public virtual IBank Handle()
    {
        return _next is null ? CentralBank.CreateBank() : _next.Handle();
    }
}