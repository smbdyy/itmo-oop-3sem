using Banks.Console.MainMenuCommandHandlers;
using Banks.Console.UserInteractionInterfaces;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SubscribeCommandHandler : BankMenuCommandHandler
{
    private readonly ICentralBank _centralBank;
    private readonly IBank _bank;

    public SubscribeCommandHandler(
        IUserInteractionInterface interactionInterface,
        ICentralBank centralBank,
        IBank bank)
        : base(interactionInterface)
    {
        _centralBank = centralBank;
        _bank = bank;
    }

    public override void Handle(string command)
    {
        if (command == "sub")
        {
            if (_centralBank.Clients.Count == 0)
            {
                InteractionInterface.WriteLine("no clients found");
                return;
            }

            BankClient client = Utils.GetClientByInputNumber(_centralBank, InteractionInterface);
            _bank.SubscribeToNotifications(client);
            return;
        }

        base.Handle(command);
    }
}