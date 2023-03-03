using Banks.Banks;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public abstract class BankMenuCommandHandler
{
    private BankMenuCommandHandler? _next;

    public BankMenuCommandHandler(
        IUserInteractionInterface interactionInterface,
        BankMenuContext context)
    {
        InteractionInterface = interactionInterface;
        Context = context;
    }

    protected IUserInteractionInterface InteractionInterface { get; }
    protected BankMenuContext Context { get; }

    public BankMenuCommandHandler SetNext(BankMenuCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public void SetBank(IBank bank)
    {
        Context.SetBank(bank);
    }

    public virtual bool Handle(string command)
    {
        if (_next is not null) return _next.Handle(command);

        InteractionInterface.WriteLine("unknown command");
        return true;
    }
}