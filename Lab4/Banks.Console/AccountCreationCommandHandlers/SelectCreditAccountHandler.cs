using Banks.Builders;
using Banks.Console.AccountCreationCommandHandlers;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public class SelectCreditAccountHandler : SelectAccountTypeCommandHandler
{
    public SelectCreditAccountHandler(
        IUserInteractionInterface interactionInterface,
        SelectAccountTypeContext context)
        : base(interactionInterface, context) { }

    public SelectCreditAccountHandler(IUserInteractionInterface interactionInterface)
        : this(interactionInterface, new SelectAccountTypeContext()) { }

    public override void Handle(string accountType)
    {
        if (accountType == "credit")
        {
            Context.AccountCreationChain
                .SetBuilder(new CreditBankAccountBuilder().SetBank(Context.Bank))
                .SetBank(Context.Bank)
                .Handle();

            return;
        }

        base.Handle(accountType);
    }
}