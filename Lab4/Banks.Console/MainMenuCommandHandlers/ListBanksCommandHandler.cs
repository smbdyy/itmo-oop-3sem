using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class ListBanksCommandHandler : MainMenuCommandHandler
{
    private readonly ICentralBank _centralBank;

    public ListBanksCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(interactionInterface) => _centralBank = centralBank;

    public override void Handle(string command)
    {
        if (command == "list_b")
        {
            if (_centralBank.Banks.Count == 0)
            {
                InteractionInterface.WriteLine("no banks found");
            }

            var banks = _centralBank.Banks.ToList();
            for (int i = 0; i < banks.Count; i++)
            {
                InteractionInterface.WriteLine($"{i}. {banks[i].Name}, id {banks[i].Id}");
            }

            return;
        }

        base.Handle(command);
    }
}