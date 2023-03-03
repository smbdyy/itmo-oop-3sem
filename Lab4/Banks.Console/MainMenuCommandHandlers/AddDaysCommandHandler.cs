using System.Text.RegularExpressions;
using Banks.CentralBanks;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.MainMenuCommandHandlers;

public class AddDaysCommandHandler : MainMenuCommandHandler
{
    private readonly ICentralBank _centralBank;

    public AddDaysCommandHandler(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
        : base(interactionInterface) => _centralBank = centralBank;

    public override bool Handle(string command)
    {
        if (!Regex.IsMatch(command, "^add_days\\s[1-9][0-9]*$")) return base.Handle(command);

        int number = Convert.ToInt32(command.Split(' ')[1]);
        for (int i = 0; i < number; i++)
        {
            _centralBank.NotifyNextDay();
        }

        InteractionInterface.WriteLine($"success, current date: {_centralBank.CurrentDate.ToString()}");
        return true;
    }
}