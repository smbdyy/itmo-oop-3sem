using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Entities;
using Banks.Tools.NotificationReceivers;

namespace Banks.Console.ClientCreationCommandHandlers;

public class ClientCreationCommandHandler
{
    private ClientCreationCommandHandler? _next;

    public ClientCreationCommandHandler(BankClientBuilder builder, IUserInteractionInterface interactionInterface)
    {
        Builder = builder;
        InteractionInterface = interactionInterface;
    }

    protected BankClientBuilder Builder { get; }
    protected IUserInteractionInterface InteractionInterface { get; }

    public ClientCreationCommandHandler SetNext(ClientCreationCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public virtual BankClient Handle()
    {
        Builder.AddNotificationReceiver(new ConsoleNotificationReceiver());
        return _next is null ? Builder.Build() : _next.Handle();
    }
}