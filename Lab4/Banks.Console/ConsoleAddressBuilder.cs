using Banks.Builders;
using Banks.Models;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Console;

public class ConsoleAddressBuilder
{
    private AddressBuilder _builder;

    public ConsoleAddressBuilder(AddressBuilder builder)
    {
        _builder = builder;
    }

    public void InputAllData()
    {
        InputCountry();
        InputTown();
        InputStreet();
        InputHouseNumber();
    }

    public void InputCountry()
    {
        System.Console.WriteLine("country:");
        while (true)
        {
            try
            {
                _builder.SetCountry(Utils.GetStringInput());
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    public void InputTown()
    {
        System.Console.WriteLine("town:");
        while (true)
        {
            try
            {
                _builder.SetTown(Utils.GetStringInput());
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    public void InputStreet()
    {
        System.Console.WriteLine("street:");
        while (true)
        {
            try
            {
                _builder.SetStreet(Utils.GetStringInput());
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    public void InputHouseNumber()
    {
        System.Console.WriteLine("house number");
        while (true)
        {
            try
            {
                _builder.SetHouseNumber(Utils.GetStringInput());
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    public Address Build()
    {
        return _builder.Build();
    }
}