using Banks.Entities;
using Banks.Models;
using Banks.Tools.Exceptions;
using Banks.Tools.NotificationReceivers;

namespace Banks.Builders;

public class BankClientBuilder
{
    private PersonName? _name;
    private Address? _address;
    private PassportNumber? _passportNumber;
    private NotificationReceiver? _notificationReceiver;

    public void Reset()
    {
        _name = null;
        _address = null;
        _passportNumber = null;
        _notificationReceiver = null;
    }

    public BankClient Build()
    {
        if (_name is null || _notificationReceiver is null)
        {
            throw new RequiredFieldInBuilderIsNullException();
        }

        return new BankClient(_notificationReceiver, _name, _passportNumber, _address);
    }

    public BankClientBuilder SetName(PersonName name)
    {
        _name = name;
        return this;
    }

    public BankClientBuilder SetAddress(Address address)
    {
        _address = address;
        return this;
    }

    public BankClientBuilder SetPassportNumber(PassportNumber passportNumber)
    {
        _passportNumber = passportNumber;
        return this;
    }

    public BankClientBuilder AddNotificationReceiver(NotificationReceiver receiver)
    {
        if (_notificationReceiver is null)
        {
            _notificationReceiver = receiver;
        }
        else
        {
            _notificationReceiver.SetNext(receiver);
        }

        return this;
    }
}