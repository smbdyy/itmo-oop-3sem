using Banks.Builders;
using Banks.Console.AccountCreationCommandHandlers;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Console.BankMenuCommandHandlers;

public class CreateAccountCommandHandler : BankMenuCommandHandler
{
    public CreateAccountCommandHandler(ICentralBank centralBank, IBank bank, IUserInteractionInterface interactionInterface)
        : base(centralBank, bank, interactionInterface) { }

    public override void Handle(string command)
    {
        if (command == "create_dep")
        {
            InteractionInterface.WriteLine("enter start money amount:");
            MoneyAmount amount = UserInputParser.GetUnsignedDecimal(InteractionInterface);
            AccountCreationCommandHandler creationHandlersChain = new SetClientCommandHandler(
                new DepositBankAccountBuilder(amount), CentralBank, InteractionInterface);

            IBankAccount account = creationHandlersChain.Handle();
            InteractionInterface.WriteLine($"account created, id: {account.Id}");

            return;
        }

        base.Handle(command);
    }
}