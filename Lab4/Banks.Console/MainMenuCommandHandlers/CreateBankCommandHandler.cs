using Banks.Banks;
using Banks.Console.BankCreationHandlers;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class CreateBankCommandHandler : MainMenuCommandHandler
{
    private readonly BankCreationCommandHandler _bankCreationChain;

    public CreateBankCommandHandler(
        IUserInteractionInterface interactionInterface, BankCreationCommandHandler bankCreationChain)
        : base(interactionInterface) => _bankCreationChain = bankCreationChain;

    public override bool Handle(string command)
    {
        if (command != "create_b") return base.Handle(command);

        IBank bank = _bankCreationChain.Handle();
        InteractionInterface.WriteLine($"bank created, id: {bank.Id}");
        return true;
    }
}