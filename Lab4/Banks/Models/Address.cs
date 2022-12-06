namespace Banks.Models;

public class Address
{
    public Address(string country, string town, string street, string houseNumber)
    {
        Country = ValidateNotEmpty(country);
        Town = ValidateNotEmpty(town);
        Street = ValidateNotEmpty(street);
        HouseNumber = ValidateNotEmpty(houseNumber);
    }

    public string Country { get; }
    public string Town { get; }
    public string Street { get; }
    public string HouseNumber { get; }

    private static string ValidateNotEmpty(string value)
    {
        if (value == string.Empty)
        {
            throw new NotImplementedException();
        }

        return value;
    }
}