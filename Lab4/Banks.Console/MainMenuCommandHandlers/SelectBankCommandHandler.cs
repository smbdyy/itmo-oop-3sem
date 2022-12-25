using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class SelectBankCommandHandler : MainMenuCommandHandler
{
    private readonly ICentralBank _centralBank;

    public SelectBankCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(interactionInterface) => _centralBank = centralBank;

    public override void Handle(string command)
    {
        if (command == "select_b")
        {
            if (_centralBank.Banks.Count == 0)
            {
                InteractionInterface.WriteLine("no banks found");
            }

            new ListBanksCommandHandler(_centralBank, InteractionInterface).Handle("list_b");
            InteractionInterface.WriteLine("enter bank number:");
            var banks = _centralBank.Banks.ToList();
            IBank bank = banks[UserInputParser.GetIntInRange(0, banks.Count, InteractionInterface)];
        }
    }
}