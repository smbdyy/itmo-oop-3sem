using Banks.Console.BankMenuCommandHandlers;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class SelectBankCommandHandler : MainMenuCommandHandler
{
    private readonly ICentralBank _centralBank;
    private readonly BankMenuCommandHandler _bankMenuChain;

    public SelectBankCommandHandler(
        ICentralBank centralBank,
        IUserInteractionInterface interactionInterface,
        BankMenuCommandHandler bankMenuChain)
        : base(interactionInterface)
    {
        _centralBank = centralBank;
        _bankMenuChain = bankMenuChain;
    }

    public override bool Handle(string command)
    {
        if (command != "select_b") return base.Handle(command);

        if (_centralBank.Banks.Count == 0)
        {
            InteractionInterface.WriteLine("no banks found");
            return true;
        }

        IBank bank = Utils.GetBankByInputNumber(_centralBank, InteractionInterface);
        InteractionInterface.WriteLine($"managing bank {bank.Name}, id {bank.Id}");
        _bankMenuChain.Handle(UserInputParser.GetLine(InteractionInterface));
        return true;
    }
}