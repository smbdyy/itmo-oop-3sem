using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console;

public class UserInputParser
{
    private IUserInteractionInterface _interactionInterface;

    public UserInputParser(IUserInteractionInterface interactionInterface)
    {
        _interactionInterface = interactionInterface;
    }

    public int GetIntInput()
    {
        string? input = _interactionInterface.ReadLine();
        while (true)
        {
            try
            {
                return Convert.ToInt32(input);
            }
            catch
            {
                _interactionInterface.WriteLine("incorrect input");
                input = _interactionInterface.ReadLine();
            }
        }
    }

    public decimal GetDecimalInput()
    {
        string? input = _interactionInterface.ReadLine();
        while (true)
        {
            try
            {
                return Convert.ToDecimal(input);
            }
            catch
            {
                _interactionInterface.WriteLine("incorrect input");
            }
        }
    }

    public string GetStringInput()
    {
        string? input = _interactionInterface.ReadLine();
        while (input is null)
        {
            _interactionInterface.WriteLine("incorrect input");
            input = _interactionInterface.ReadLine();
        }

        return input;
    }

    public bool GetYesNoAnswerAsBool()
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
                    _interactionInterface.WriteLine("incorrect input");
                    break;
            }
        }
    }
}