using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public class SelectDebitAccountHandler : SelectAccountTypeCommandHandler
{
    public SelectDebitAccountHandler(
        ICentralBank centralBank, IBank bank, IUserInteractionInterface interactionInterface)
        : base(centralBank, bank, interactionInterface) { }

    public override void Handle(string accountType)
    {
        if (accountType == "debit")
        {
            AccountCreationCommandHandler creationHandlersChain = new SetClientCommandHandler(
                new DebitBankAccountBuilder().SetBank(Bank), CentralBank, InteractionInterface);

            IBankAccount account = creationHandlersChain.Handle();
            InteractionInterface.WriteLine($"account created, id: {account.Id}");

            return;
        }

        base.Handle(accountType);
    }
}