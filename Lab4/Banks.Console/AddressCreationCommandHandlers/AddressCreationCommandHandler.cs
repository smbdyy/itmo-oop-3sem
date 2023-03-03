using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Models;
using Banks.Models.Builders;

namespace Banks.Console.AddressCreationCommandHandlers;

public class AddressCreationCommandHandler
{
    private AddressCreationCommandHandler? _next;

    public AddressCreationCommandHandler(AddressBuilder builder, IUserInteractionInterface interactionInterface)
    {
        Builder = builder;
        InteractionInterface = interactionInterface;
    }

    protected AddressBuilder Builder { get; }
    protected IUserInteractionInterface InteractionInterface { get; }

    public AddressCreationCommandHandler SetNext(AddressCreationCommandHandler next)
    {
        _next = next;
        return _next;
    }

    public virtual Address Handle()
    {
        return _next is null ? Builder.Build() : _next.Handle();
    }
}