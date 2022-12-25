using Banks.Console.BankCreationHandlers;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class CreateBankCommandHandler : MainMenuCommandHandler
{
    private readonly BankCreationCommandHandler _bankCreationChain;

    public CreateBankCommandHandler(
        IUserInteractionInterface interactionInterface, BankCreationCommandHandler bankCreationChain)
        : base(interactionInterface) => _bankCreationChain = bankCreationChain;

    public override void Handle(string command)
    {
        if (command == "create_b")
        {
            IBank bank = _bankCreationChain.Handle();
            InteractionInterface.WriteLine($"bank created, id: {bank.Id}");
            return;
        }

        base.Handle(command);
    }
}