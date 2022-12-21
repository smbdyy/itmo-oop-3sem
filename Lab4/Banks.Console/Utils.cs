namespace Banks.Console;

public static class Utils
{
    public static int GetIntInput()
    {
        string? input = System.Console.ReadLine();
        while (true)
        {
            try
            {
                return Convert.ToInt32(input);
            }
            catch
            {
                System.Console.WriteLine("incorrect input");
                input = System.Console.ReadLine();
            }
        }
    }

    public static decimal GetDecimalInput()
    {
        string? input = System.Console.ReadLine();
        while (true)
        {
            try
            {
                decimal value = Convert.ToDecimal(input);
                if (value >= 0)
                {
                    return value;
                }
            }
            catch
            {
                System.Console.WriteLine("incorrect input");
            }
        }
    }

    public static string GetStringInput()
    {
        string? input = System.Console.ReadLine();
        while (input is null)
        {
            System.Console.WriteLine("incorrect input");
            input = System.Console.ReadLine();
        }

        return input;
    }

    public static bool GetYesNoAnswerAsBool()
    {
        string input = GetStringInput();
        while (true)
        {
            switch (input)
            {
                case "y":
                    return true;
                case "n":
                    return false;
                default:
                    System.Console.WriteLine("incorrect input");
                    break;
            }
        }
    }
}