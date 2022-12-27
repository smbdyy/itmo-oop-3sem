using Banks.Console.UserInteractionInterfaces;
using Banks.Entities;

namespace Banks.Console.ClientMenuCommandHandlers;

public abstract class ClientMenuCommandHandler
{
    private ClientMenuCommandHandler? _next;

    public ClientMenuCommandHandler(IUserInteractionInterface interactionInterface)
        => InteractionInterface = interactionInterface;

    protected IUserInteractionInterface InteractionInterface { get; }
    protected BankClient? Client { get; private set; }

    public ClientMenuCommandHandler SetNext(ClientMenuCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public ClientMenuCommandHandler SetClient(BankClient client)
    {
        Client = client;
        _next?.SetClient(client);
        return this;
    }

    public virtual bool Handle(string command)
    {
        if (_next is not null) return _next.Handle(command);

        InteractionInterface.WriteLine("unknown command");
        return true;
    }
}