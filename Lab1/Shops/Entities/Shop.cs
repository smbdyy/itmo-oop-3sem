using Shops.Exceptions;

namespace Shops.Entities;

public class Shop
{
    private List<ShopProductInfo> _shopItems = new ();
    private string _name;
    private string _address;

    public Shop(string name, string address)
    {
        _name = Validate(name);
        _address = Validate(address);
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }

    public string Name
    {
        get => _name;
        set => _name = Validate(value);
    }

    public string Address
    {
        get => _address;
        set => _address = Validate(value);
    }

    private static string Validate(string value)
    {
        if (value == string.Empty)
        {
            throw new EmptyNameStringException();
        }

        return value;
    }
}