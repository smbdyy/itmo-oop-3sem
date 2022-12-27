using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.ClientMenuCommandHandlers;

public class ClientInfoCommandHandler : ClientMenuCommandHandler
{
    public ClientInfoCommandHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override bool Handle(string command)
    {
       if (command != "info") return base.Handle(command);

       string address = Client!.Address is null ? "---" : Client.Address.AsString;
       string passportNumber = Client!.PassportNumber is null ? "---" : Client.PassportNumber.AsString;

       InteractionInterface.WriteLine(
           $@"Name: {Client.Name.AsString}
              Id: {Client.Id}
              Address: {address}
              PassportNumber: {passportNumber}");
       return true;
    }
}