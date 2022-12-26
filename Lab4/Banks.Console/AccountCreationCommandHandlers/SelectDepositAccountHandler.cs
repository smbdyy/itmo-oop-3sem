using Banks.Builders;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Console.AccountCreationCommandHandlers;

public class SelectDepositAccountHandler : SelectAccountTypeCommandHandler
{
    public SelectDepositAccountHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override void Handle(string accountType)
    {
        if (accountType == "deposit")
        {
            InteractionInterface.WriteLine("enter start money amount:");
            MoneyAmount amount = UserInputParser.GetUnsignedDecimal(InteractionInterface);
            AccountCreationChain!
                .SetBuilder(new DepositBankAccountBuilder().SetStartMoneyAmount(amount))
                .Handle();

            return;
        }

        base.Handle(accountType);
    }
}