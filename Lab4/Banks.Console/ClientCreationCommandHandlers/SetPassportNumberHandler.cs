using Banks.Builders;
using Banks.Clients;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Models;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Console.ClientCreationCommandHandlers;

public class SetPassportNumberHandler : ClientCreationCommandHandler
{
    public SetPassportNumberHandler(BankClientBuilder builder, IUserInteractionInterface interactionInterface)
        : base(builder, interactionInterface) { }

    public override BankClient Handle()
    {
        InteractionInterface.WriteLine("do you want to enter the passport number? (y/n)");
        if (!UserInputParser.GetYesNoAnswerAsBool(InteractionInterface))
        {
            return base.Handle();
        }

        while (true)
        {
            try
            {
                Builder.SetPassportNumber(PassportNumber
                    .FromString(UserInputParser.GetLine(InteractionInterface)));
                return base.Handle();
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }
}