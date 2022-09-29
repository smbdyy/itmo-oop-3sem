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

    public void Buy(Person person, List<OrderItem> order)
    {
        int cost = 0;
        foreach (OrderItem orderItem in order)
        {
            ShopProductInfo? shopItem = _shopItems.FirstOrDefault(item => item.Product == orderItem.Product);
            if (shopItem == null || shopItem.Amount < orderItem.Amount)
            {
                throw new NotEnoughProductException(this, orderItem.Product);
            }

            cost += orderItem.Amount * shopItem.Price;
        }

        if (cost > person.Money)
        {
            throw new NotEnoughMoneyException(person, cost);
        }

        person.Money -= cost;

        foreach (OrderItem orderItem in order)
        {
            ShopProductInfo shopItem = _shopItems.First(item => item.Product == orderItem.Product);
            shopItem.Amount -= orderItem.Amount;
        }
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