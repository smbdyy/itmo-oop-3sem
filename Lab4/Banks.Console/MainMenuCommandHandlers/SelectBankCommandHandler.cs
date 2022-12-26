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
                return;
            }

            IBank bank = Utils.GetBankByInputNumber(_centralBank, InteractionInterface);

            // TODO bank menu
            return;
        }

        base.Handle(command);
    }
}