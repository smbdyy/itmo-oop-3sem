using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.AccountMenuCommandHandlers;

public class AccountInfoCommandHandler : AccountMenuCommandHandler
{
    public AccountInfoCommandHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override bool Handle(string command)
    {
        if (command != "info") return base.Handle(command);

        InteractionInterface.WriteLine(
            @$"id {Account!.Id}
                    client {Account.Client.Name.AsString}
                    money amount {Account.MoneyAmount}
                    ");

        return true;
    }
}