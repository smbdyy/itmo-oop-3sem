using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.ClientMenuCommandHandlers;

public class ClientInfoCommandHandler : ClientMenuCommandHandler
{
    public ClientInfoCommandHandler(IUserInteractionInterface interactionInterface, ClientMenuContext context)
        : base(interactionInterface, context) { }

    public override bool Handle(string command)
    {
       if (command != "info") return base.Handle(command);

       string address = Context.Client.Address is null ? "---" : Context.Client.Address.AsString;
       string passportNumber = Context.Client.PassportNumber is null ? "---" : Context.Client.PassportNumber.AsString;

       InteractionInterface.WriteLine(
           $@"Name: {Context.Client.Name.AsString}
              Id: {Context.Client.Id}
              Address: {address}
              PassportNumber: {passportNumber}");
       return true;
    }
}