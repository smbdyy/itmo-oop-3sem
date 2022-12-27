using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public class SelectDebitAccountHandler : SelectAccountTypeCommandHandler
{
    public SelectDebitAccountHandler(
        IUserInteractionInterface interactionInterface,
        SelectAccountTypeContext context)
        : base(interactionInterface, context) { }

    public SelectDebitAccountHandler(IUserInteractionInterface interactionInterface)
        : this(interactionInterface, new SelectAccountTypeContext()) { }

    public override void Handle(string accountType)
    {
        if (accountType == "debit")
        {
            Context.AccountCreationChain
                .SetBuilder(new DebitBankAccountBuilder().SetBank(Context.Bank))
                .SetBank(Context.Bank)
                .Handle();

            return;
        }

        base.Handle(accountType);
    }
}