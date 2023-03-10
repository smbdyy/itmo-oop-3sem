using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.AccountMenuCommandHandlers;

public class ExitAccountMenuCommandHandler : AccountMenuCommandHandler
{
    public ExitAccountMenuCommandHandler(
        IUserInteractionInterface interactionInterface,
        AccountMenuContext context)
        : base(interactionInterface, context) { }

    public override bool Handle(string command)
    {
        if (command != "exit") return base.Handle(command);

        InteractionInterface.WriteLine("returning to bank menu");
        return false;
    }
}