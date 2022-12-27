using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.Tools;

public static class UserInputParser
{
    public static int GetIntInRange(int left, int right, IUserInteractionInterface interactionInterface)
    {
        while (true)
        {
            int value = GetUnsignedInt(interactionInterface);
            if (value >= left && value <= right)
            {
                return value;
            }

            interactionInterface.WriteLine($"value must be in range [{left}; {right})");
        }
    }

    public static int GetUnsignedInt(IUserInteractionInterface interactionInterface)
    {
        while (true)
        {
            try
            {
                int value = Convert.ToInt32(interactionInterface.ReadLine());
                if (value >= 0) return value;
            }
            catch
            {
                interactionInterface.WriteLine("incorrect input");
            }
        }
    }

    public static decimal GetUnsignedDecimal(IUserInteractionInterface interactionInterface)
    {
        while (true)
        {
            try
            {
                decimal value = Convert.ToDecimal(interactionInterface.ReadLine());
                if (value >= 0) return value;
            }
            catch
            {
                // ignored
            }

            interactionInterface.WriteLine("incorrect input");
        }
    }

    public static decimal GetNonPositiveDecimal(IUserInteractionInterface interactionInterface)
    {
        while (true)
        {
            try
            {
                decimal value = Convert.ToDecimal(interactionInterface.ReadLine());
                if (value <= 0) return value;
            }
            catch
            {
                // ignored
            }

            interactionInterface.WriteLine("incorrect input");
        }
    }

    public static string GetLine(IUserInteractionInterface interactionInterface)
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
        while (true)
        {
            string input = GetLine(interactionInterface);
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