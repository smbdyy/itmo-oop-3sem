using Banks.Console.AccountCreationCommandHandlers;
using Banks.Console.BankCreationHandlers;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class CreateAccountCommandHandler : BankMenuCommandHandler
{
    private readonly SelectAccountTypeCommandHandler _selectAccountTypeChain;
    private readonly ICentralBank _centralBank;

    public CreateAccountCommandHandler(
        IUserInteractionInterface interactionInterface,
        SelectAccountTypeCommandHandler selectAccountTypeChain,
        ICentralBank centralBank,
        BankMenuContext context)
        : base(interactionInterface, context)
    {
        _selectAccountTypeChain = selectAccountTypeChain;
        _centralBank = centralBank;
    }

    public override bool Handle(string command)
    {
        if (command != "create_acc") return base.Handle(command);

        if (_centralBank.Clients.Count == 0)
        {
            InteractionInterface.WriteLine("no clients found");
            return true;
        }

        InteractionInterface.WriteLine("enter account type:");
        _selectAccountTypeChain.SetBank(Context.Bank);
        _selectAccountTypeChain.Handle(UserInputParser.GetLine(InteractionInterface));
        return true;
    }
}