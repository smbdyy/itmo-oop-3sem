using Banks.Builders;
using Banks.Console.Tools;
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
                .SetBank(Bank!)
                .Handle();

            return;
        }

        base.Handle(accountType);
    }
}