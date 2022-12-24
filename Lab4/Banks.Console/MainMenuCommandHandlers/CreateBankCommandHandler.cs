using Banks.Console.BankCreationHandlers;
using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class CreateBankCommandHandler : MainMenuCommandHandler
{
    public CreateBankCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(centralBank, interactionInterface) { }

    public override void Handle(string command)
    {
        if (command == "create_b")
        {
            BankCreationCommandHandler creationHandlersChain = new SetNameHandler(CentralBank, InteractionInterface);
            creationHandlersChain
                .SetNext(new SetCreditAccountCommissionHandler(CentralBank, InteractionInterface))
                .SetNext(new SetCreditAccountLimitHandler(CentralBank, InteractionInterface))
                .SetNext(new SetDepositAccountTermHandler(CentralBank, InteractionInterface))
                .SetNext(new SetUnverifiedClientWithdrawalLimitHandler(CentralBank, InteractionInterface));

            creationHandlersChain.Handle();
            return;
        }

        base.Handle(command);
    }
}