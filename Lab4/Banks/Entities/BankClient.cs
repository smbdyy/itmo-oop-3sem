using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class BankClient
{
    public BankClient(INotificationReceiver notificationReceiver, PersonName name, PassportNumber? passportNumber, Address? address)
    {
        Name = name;
        PassportNumber = passportNumber;
        Address = address;
        NotificationReceiver = notificationReceiver;
    }

    public BankClient(INotificationReceiver notificationReceiver, PersonName name)
    {
        Name = name;
        NotificationReceiver = notificationReceiver;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public PersonName Name { get; }
    public string NameAsString => $"{Name.Name} + {Name.Surname}";
    public PassportNumber? PassportNumber { get; set; }
    public Address? Address { get; set; }
    public INotificationReceiver NotificationReceiver { get; }
}