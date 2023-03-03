using Banks.Clients;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.ClientMenuCommandHandlers;

public abstract class ClientMenuCommandHandler
{
    private ClientMenuCommandHandler? _next;

    public ClientMenuCommandHandler(
        IUserInteractionInterface interactionInterface,
        ClientMenuContext context)
    {
        InteractionInterface = interactionInterface;
        Context = context;
    }

    protected IUserInteractionInterface InteractionInterface { get; }
    protected ClientMenuContext Context { get; }

    public ClientMenuCommandHandler SetNext(ClientMenuCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public ClientMenuCommandHandler SetClient(BankClient client)
    {
        Context.SetClient(client);
        return this;
    }

    public virtual bool Handle(string command)
    {
        if (_next is not null) return _next.Handle(command);

        InteractionInterface.WriteLine("unknown command");
        return true;
    }
}