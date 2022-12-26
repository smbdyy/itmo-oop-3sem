using Banks.Console.AccountCreationCommandHandlers;
using Banks.Console.BankCreationHandlers;
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
        ICentralBank centralBank)
        : base(interactionInterface)
    {
        _selectAccountTypeChain = selectAccountTypeChain;
        _centralBank = centralBank;
    }

    public override void Handle(string command)
    {
        if (command == "create_acc")
        {
            if (_centralBank.Clients.Count == 0)
            {
                InteractionInterface.WriteLine("no client found");
                return;
            }

            InteractionInterface.WriteLine("enter account type:");
            _selectAccountTypeChain.Handle(UserInputParser.GetLine(InteractionInterface));
            return;
        }

        base.Handle(command);
    }
}