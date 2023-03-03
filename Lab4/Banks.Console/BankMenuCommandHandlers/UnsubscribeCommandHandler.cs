using Banks.Clients;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class UnsubscribeCommandHandler : BankMenuCommandHandler
{
    public UnsubscribeCommandHandler(IUserInteractionInterface interactionInterface, BankMenuContext context)
        : base(interactionInterface, context) { }

    public override bool Handle(string command)
    {
        if (command != "unsub") return base.Handle(command);

        if (Context.Bank.Subscribers.Count == 0)
        {
            InteractionInterface.WriteLine("no subscribers found");
            return true;
        }

        var subscribers = Context.Bank.Subscribers.ToList();
        for (int i = 0; i < subscribers.Count; i++)
        {
            InteractionInterface.WriteLine($"{i}. {subscribers[i].Name.AsString}, id {subscribers[i].Id}");
        }

        InteractionInterface.WriteLine("enter client number:");
        int number = UserInputParser.GetIntInRange(0, subscribers.Count, InteractionInterface);
        BankClient subscriber = subscribers[number];
        Context.Bank.UnsubscribeFromNotifications(subscriber);
        InteractionInterface.WriteLine("unsubscribed");

        return base.Handle(command);
    }
}