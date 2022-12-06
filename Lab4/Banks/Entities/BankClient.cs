using Banks.Models;

namespace Banks.Entities;

public class BankClient
{
    public BankClient(PersonName name, PassportNumber passportNumber, Address address)
    {
        Name = name;
        PassportNumber = passportNumber;
        Address = address;
    }

    public BankClient(PersonName name)
    {
        Name = name;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public PersonName Name { get; }
    public PassportNumber? PassportNumber { get; set; }
    public Address? Address { get; set; }
}