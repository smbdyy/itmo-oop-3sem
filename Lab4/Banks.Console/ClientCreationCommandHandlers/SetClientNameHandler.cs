using Banks.Builders;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Entities;
using Banks.Models;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Console.ClientCreationCommandHandlers;

public class SetClientNameHandler : ClientCreationCommandHandler
{
    public SetClientNameHandler(BankClientBuilder builder, IUserInteractionInterface interactionInterface)
        : base(builder, interactionInterface) { }

    public override BankClient Handle()
    {
        InteractionInterface.WriteLine("enter client name and surname:");
        while (true)
        {
            string value = UserInputParser.GetLine(InteractionInterface);
            try
            {
                Builder.SetName(PersonName.FromString(value));
                return base.Handle();
            }
            catch (ArgumentException ex)
            {
                InteractionInterface.WriteLine($"exception: {ex.Message}");
            }
        }
    }
}