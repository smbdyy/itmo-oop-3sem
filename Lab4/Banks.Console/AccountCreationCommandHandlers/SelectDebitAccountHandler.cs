using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public class SelectDebitAccountHandler : SelectAccountTypeCommandHandler
{
    public SelectDebitAccountHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override void Handle(string accountType)
    {
        if (accountType == "debit")
        {
            AccountCreationChain!
                .SetBuilder(new DebitBankAccountBuilder().SetBank(Bank!))
                .SetBank(Bank!)
                .Handle();

            return;
        }

        base.Handle(accountType);
    }
}