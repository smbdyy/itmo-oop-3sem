using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class ListBanksCommandHandler : MainMenuCommandHandler
{
    public ListBanksCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }

    public override void Handle(string command)
    {
        if (command == "list_b")
        {
            if (CentralBank.Banks.Count == 0)
            {
                InteractionInterface.WriteLine("no banks found");
            }

            var banks = CentralBank.Banks.ToList();
            for (int i = 0; i < banks.Count; i++)
            {
                InteractionInterface.WriteLine($"{i}. {banks[i].Name}, id {banks[i].Id}");
            }

            return;
        }

        base.Handle(command);
    }
}