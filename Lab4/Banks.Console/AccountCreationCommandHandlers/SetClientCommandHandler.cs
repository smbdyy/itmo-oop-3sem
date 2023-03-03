using Banks.Builders;
using Banks.CentralBanks;
using Banks.Clients;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.AccountCreationCommandHandlers;

public class SetClientCommandHandler : AccountCreationCommandHandler
{
    private readonly ICentralBank _centralBank;

    public SetClientCommandHandler(
        ICentralBank centralBank,
        IUserInteractionInterface interactionInterface,
        AccountCreationContext context)
        : base(interactionInterface, context)
    {
        _centralBank = centralBank;
    }

    public override void Handle()
    {
        InteractionInterface.WriteLine("select client:");
        BankClient client = Utils.GetClientByInputNumber(_centralBank, InteractionInterface);
        Context.Builder.SetClient(client);
        base.Handle();
    }
}