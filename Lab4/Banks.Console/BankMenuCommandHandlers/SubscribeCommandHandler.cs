using Banks.Console.MainMenuCommandHandlers;
using Banks.Console.UserInteractionInterfaces;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SubscribeCommandHandler : BankMenuCommandHandler
{
    private readonly ICentralBank _centralBank;

    public SubscribeCommandHandler(
        IUserInteractionInterface interactionInterface,
        ICentralBank centralBank)
        : base(interactionInterface)
    {
        _centralBank = centralBank;
    }

    public override bool Handle(string command)
    {
        if (command != "sub") return base.Handle(command);

        if (_centralBank.Clients.Count == 0)
        {
            InteractionInterface.WriteLine("no clients found");
            return true;
        }

        BankClient client = Utils.GetClientByInputNumber(_centralBank, InteractionInterface);
        Bank!.SubscribeToNotifications(client);
        return true;
    }
}