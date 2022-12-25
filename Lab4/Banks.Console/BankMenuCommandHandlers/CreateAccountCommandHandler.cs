using Banks.Console.AccountCreationCommandHandlers;
using Banks.Console.BankCreationHandlers;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class CreateAccountCommandHandler : BankMenuCommandHandler
{
    private SelectAccountTypeCommandHandler _selectAccountTypeChain;

    public CreateAccountCommandHandler(
        IUserInteractionInterface interactionInterface, SelectAccountTypeCommandHandler selectAccountTypeChain)
        : base(interactionInterface) => _selectAccountTypeChain = selectAccountTypeChain;

    public override void Handle(string command)
    {
        if (command == "create_acc")
        {
            InteractionInterface.WriteLine("enter account type:");
            _selectAccountTypeChain.Handle(UserInputParser.GetLine(InteractionInterface));

            return;
        }

        base.Handle(command);
    }
}