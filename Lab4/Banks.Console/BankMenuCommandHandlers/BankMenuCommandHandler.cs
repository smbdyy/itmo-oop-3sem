using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public abstract class BankMenuCommandHandler
{
    private BankMenuCommandHandler? _next;

    public BankMenuCommandHandler(IUserInteractionInterface interactionInterface)
    {
        InteractionInterface = interactionInterface;
    }

    protected IUserInteractionInterface InteractionInterface { get; }
    protected IBank? Bank { get; private set; }

    public BankMenuCommandHandler SetNext(BankMenuCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public void SetBank(IBank bank)
    {
        Bank = bank;
        _next?.SetBank(bank);
    }

    public virtual bool Handle(string command)
    {
        if (_next is not null) return _next.Handle(command);

        InteractionInterface.WriteLine("unknown command");
        return true;
    }
}