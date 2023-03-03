using Banks.Tools.Exceptions;

namespace Banks.Models.Builders;

public class AddressBuilder
{
    private string? _country;
    private string? _town;
    private string? _street;
    private string? _houseNumber;

    public void Reset()
    {
        _country = null;
        _town = null;
        _street = null;
        _houseNumber = null;
    }

    public Address Build()
    {
        if (_country is null || _town is null || _street is null || _houseNumber is null)
        {
            throw new RequiredFieldInBuilderIsNullException();
        }

        return new Address(_country, _town, _street, _houseNumber);
    }

    public AddressBuilder SetCountry(string country)
    {
        _country = country;
        return this;
    }

    public AddressBuilder SetTown(string town)
    {
        _town = town;
        return this;
    }

    public AddressBuilder SetStreet(string street)
    {
        _street = street;
        return this;
    }

    public AddressBuilder SetHouseNumber(string houseNumber)
    {
        _houseNumber = houseNumber;
        return this;
    }
}