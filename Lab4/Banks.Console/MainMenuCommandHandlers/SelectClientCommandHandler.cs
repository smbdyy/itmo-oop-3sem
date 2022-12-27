using Banks.Console.ClientMenuCommandHandlers;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class SelectClientCommandHandler : MainMenuCommandHandler
{
    private readonly ICentralBank _centralBank;
    private readonly ClientMenuCommandHandler _clientMenuChain;

    public SelectClientCommandHandler(
        IUserInteractionInterface interactionInterface,
        ICentralBank centralBank,
        ClientMenuCommandHandler clientMenuChain)
        : base(interactionInterface)
    {
        _centralBank = centralBank;
        _clientMenuChain = clientMenuChain;
    }

    public override bool Handle(string command)
    {
        if (command != "select_c") return base.Handle(command);

        BankClient client = Utils.GetClientByInputNumber(_centralBank, InteractionInterface);
        _clientMenuChain.SetClient(client);
        InteractionInterface.WriteLine($"managing client {client.Name.AsString}, id {client.Id}");
        while (_clientMenuChain.Handle(UserInputParser.GetLine(InteractionInterface)))
        {
        }

        return true;
    }
}