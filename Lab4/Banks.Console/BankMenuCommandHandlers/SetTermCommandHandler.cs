using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.BankMenuCommandHandlers;

public class SetTermCommandHandler : BankMenuCommandHandler
{
    public SetTermCommandHandler(IUserInteractionInterface interactionInterface, BankMenuContext context)
        : base(interactionInterface, context) { }

    public SetTermCommandHandler(IUserInteractionInterface interactionInterface)
        : this(interactionInterface, new BankMenuContext()) { }

    public override bool Handle(string command)
    {
        if (command != "set_term") return base.Handle(command);

        InteractionInterface.WriteLine("enter new deposit account term:");
        Context.Bank.DepositAccountTerm = UserInputParser.GetUnsignedInt(InteractionInterface);
        return true;
    }
}