using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class SelectBankCommandHandler : MainMenuCommandHandler
{
    public SelectBankCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }

    public override void Handle(string command)
    {
        if (command == "select_b")
        {
            if (CentralBank.Banks.Count == 0)
            {
                InteractionInterface.WriteLine("no banks found");
            }

            new ListBanksCommandHandler(CentralBank, InteractionInterface).Handle("list_b");
            InteractionInterface.WriteLine("enter bank number:");
            var banks = CentralBank.Banks.ToList();
            IBank bank = banks[UserInputParser.GetIntInRange(0, banks.Count, InteractionInterface)];
        }
    }
}