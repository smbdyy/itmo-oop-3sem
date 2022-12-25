using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Console.AccountCreationCommandHandlers;

public class SelectDepositAccountHandler : SelectAccountTypeCommandHandler
{
    public SelectDepositAccountHandler(
        ICentralBank centralBank, IBank bank, IUserInteractionInterface interactionInterface)
        : base(centralBank, bank, interactionInterface) { }

    public override void Handle(string accountType)
    {
        if (accountType == "deposit")
        {
            InteractionInterface.WriteLine("enter start money amount:");
            MoneyAmount amount = UserInputParser.GetUnsignedDecimal(InteractionInterface);
            AccountCreationCommandHandler creationHandlersChain = new SetClientCommandHandler(
                new DepositBankAccountBuilder(amount).SetBank(Bank), CentralBank, InteractionInterface);

            IBankAccount account = creationHandlersChain.Handle();
            InteractionInterface.WriteLine($"account created, id: {account.Id}");

            return;
        }

        base.Handle(accountType);
    }
}