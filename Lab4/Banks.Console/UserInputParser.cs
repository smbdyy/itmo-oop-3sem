using Banks.Console.UserInteractionInterfaces;
using Banks.Models;

namespace Banks.Console;

public static class UserInputParser
{
    public static DepositTermDays GetDepositTermDaysInput(IUserInteractionInterface interactionInterface)
    {
        string? input = interactionInterface.ReadLine();
        while (true)
        {
            try
            {
                return Convert.ToInt32(input);
            }
            catch
            {
                interactionInterface.WriteLine("incorrect input");
                input = interactionInterface.ReadLine();
            }
        }
    }

    public static MoneyAmount GetMoneyAmountInput(IUserInteractionInterface interactionInterface)
    {
        string? input = interactionInterface.ReadLine();
        while (true)
        {
            try
            {
                return Convert.ToDecimal(input);
            }
            catch
            {
                interactionInterface.WriteLine("incorrect input");
            }
        }
    }

    public static NonPositiveMoneyAmount GetNonPositiveMoneyAmount(IUserInteractionInterface interactionInterface)
    {
        string? input = interactionInterface.ReadLine();
        while (true)
        {
            try
            {
                return Convert.ToDecimal(input);
            }
            catch
            {
                interactionInterface.WriteLine("incorrect input");
            }
        }
    }

    public static string GetStringInput(IUserInteractionInterface interactionInterface)
    {
        string? input = interactionInterface.ReadLine();
        while (input is null || input == string.Empty)
        {
            interactionInterface.WriteLine("incorrect input");
            input = interactionInterface.ReadLine();
        }

        return input;
    }

    public static bool GetYesNoAnswerAsBool(IUserInteractionInterface interactionInterface)
    {
        string input = GetStringInput(interactionInterface);
        while (true)
        {
            switch (input)
            {
                case "y":
                    return true;
                case "n":
                    return false;
                default:
                    interactionInterface.WriteLine("incorrect input");
                    break;
            }
        }
    }
}