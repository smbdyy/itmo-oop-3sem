using Banks.Builders;
using Banks.Console.AccountCreationCommandHandlers;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public class SelectCreditAccountHandler : SelectAccountTypeCommandHandler
{
    public SelectCreditAccountHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override void Handle(string accountType)
    {
        if (accountType == "credit")
        {
            AccountCreationChain!
                .SetBuilder(new CreditBankAccountBuilder().SetBank(Bank!))
                .SetBank(Bank!)
                .Handle();

            return;
        }

        base.Handle(accountType);
    }
}