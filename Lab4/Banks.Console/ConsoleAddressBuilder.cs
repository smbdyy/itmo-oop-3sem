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

    public void InputCountry()
    {
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

    public Address Build()
    {
        return _builder.Build();
    }
}