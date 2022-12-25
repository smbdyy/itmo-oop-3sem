using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public abstract class SelectAccountTypeCommandHandler
{
    private SelectAccountTypeCommandHandler? _next;

    public SelectAccountTypeCommandHandler(
        ICentralBank centralBank, IBank bank, IUserInteractionInterface interactionInterface)
    {
        CentralBank = centralBank;
        Bank = bank;
        InteractionInterface = interactionInterface;
    }

    protected ICentralBank CentralBank { get; }
    protected IBank Bank { get; }
    protected IUserInteractionInterface InteractionInterface { get; }

    public SelectAccountTypeCommandHandler SetNext(SelectAccountTypeCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public virtual void Handle(string accountType)
    {
        if (_next is null)
        {
            InteractionInterface.WriteLine("unknown account type");
            return;
        }

        _next.Handle(accountType);
    }
}