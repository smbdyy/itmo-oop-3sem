using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.AccountMenuCommandHandlers;

public class AccountInfoCommandHandler : AccountMenuCommandHandler
{
    public AccountInfoCommandHandler(
        IUserInteractionInterface interactionInterface,
        AccountMenuContext context)
        : base(interactionInterface, context) { }

    public override bool Handle(string command)
    {
        if (command != "info") return base.Handle(command);

        InteractionInterface.WriteLine(
            @$"id {Context.Account.Id}
                    client {Context.Account.Client.Name.AsString}
                    money amount {Context.Account.MoneyAmount}
                    ");

        return true;
    }
}