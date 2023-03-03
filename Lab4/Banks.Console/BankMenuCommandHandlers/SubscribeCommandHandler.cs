using Banks.CentralBanks;
using Banks.Clients;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SubscribeCommandHandler : BankMenuCommandHandler
{
    private readonly ICentralBank _centralBank;

    public SubscribeCommandHandler(
        IUserInteractionInterface interactionInterface,
        ICentralBank centralBank,
        BankMenuContext context)
        : base(interactionInterface, context)
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
        Context.Bank.SubscribeToNotifications(client);
        InteractionInterface.WriteLine("subscribed!");
        return true;
    }
}