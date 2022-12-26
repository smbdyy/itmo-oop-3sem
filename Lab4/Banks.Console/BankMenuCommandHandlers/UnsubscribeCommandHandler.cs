using Banks.Console.UserInteractionInterfaces;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class UnsubscribeCommandHandler : BankMenuCommandHandler
{
    private readonly IBank _bank;

    public UnsubscribeCommandHandler(IUserInteractionInterface interactionInterface, IBank bank)
        : base(interactionInterface) => _bank = bank;

    public override bool Handle(string command)
    {
        if (command != "unsub") return base.Handle(command);

        if (_bank.Subscribers.Count == 0)
        {
            InteractionInterface.WriteLine("no subscribers found");
            return true;
        }

        var subscribers = _bank.Subscribers.ToList();
        for (int i = 0; i < subscribers.Count; i++)
        {
            InteractionInterface.WriteLine($"{i}. {subscribers[i].Name.AsString}, id {subscribers[i].Id}");
        }

        InteractionInterface.WriteLine("enter client number:");
        int number = UserInputParser.GetIntInRange(0, subscribers.Count, InteractionInterface);
        BankClient subscriber = subscribers[number];
        _bank.UnsubscribeFromNotifications(subscriber);

        return base.Handle(command);
    }
}