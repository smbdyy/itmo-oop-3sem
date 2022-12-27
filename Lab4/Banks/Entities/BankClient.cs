using Banks.Models;
using Banks.Tools.NotificationReceivers;

namespace Banks.Entities;

public class BankClient
{
    public BankClient(NotificationReceiver notificationReceiver, PersonName name, PassportNumber? passportNumber, Address? address)
    {
        Name = name;
        PassportNumber = passportNumber;
        Address = address;
        NotificationReceiver = notificationReceiver;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public PersonName Name { get; }
    public PassportNumber? PassportNumber { get; set; }
    public Address? Address { get; set; }
    public NotificationReceiver NotificationReceiver { get; }
}