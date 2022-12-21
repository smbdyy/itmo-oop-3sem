using Banks.Builders;
using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;
using Banks.Tools.NotificationReceivers;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Console;

public class ConsoleClientBuilder
{
    private ICentralBank _centralBank;
    private BankClientBuilder _builder;

    public ConsoleClientBuilder(ICentralBank centralBank, BankClientBuilder builder)
    {
        _centralBank = centralBank;
        _builder = builder;
        _builder.AddNotificationReceiver(new ConsoleNotificationReceiver());
    }

    public void InputAllData()
    {
        InputName();
        InputAddress();
        InputPassportNumber();
    }

    public void InputName()
    {
        System.Console.WriteLine("input name:");
        while (true)
        {
            string value = Utils.GetStringInput();
            try
            {
                _builder.SetName(PersonName.FromString(value));
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    public void InputAddress()
    {
        System.Console.WriteLine("do you want to input the address? (y/n)");
        if (!Utils.GetYesNoAnswerAsBool()) return;

        var builder = new ConsoleAddressBuilder(new AddressBuilder());
        builder.InputAllData();
        _builder.SetAddress(builder.Build());
    }

    public void InputPassportNumber()
    {
        System.Console.WriteLine("do you want to input the password number? (y/n)");
        if (!Utils.GetYesNoAnswerAsBool()) return;

        while (true)
        {
            try
            {
                _builder.SetPassportNumber(PassportNumber.FromString(Utils.GetStringInput()));
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    public BankClient Build()
    {
        BankClient client = _builder.Build();
        _centralBank.RegisterClient(client);
        return client;
    }
}