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
        foreach (OrderItem orderItem in order)
        {
            ShopProductInfo? shopItem = _shopItems.FirstOrDefault(item => item.Product == orderItem.Product);
            if (shopItem == null || shopItem.Amount < orderItem.Amount)
            {
                throw new NotEnoughProductException(this, orderItem.Product);
            }
        }

        decimal cost = CountOrderCost(order);
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

    public void Supply(List<ShopProductInfo> supplement)
    {
        foreach (ShopProductInfo supplementItem in supplement)
        {
            ShopProductInfo? shopItem = _shopItems.FirstOrDefault(item => item.Product == supplementItem.Product);
            if (shopItem == null)
            {
                _shopItems.Add(supplementItem);
            }
            else
            {
                shopItem.Amount += supplementItem.Amount;
                shopItem.Price = supplementItem.Price;
            }
        }
    }

    public void ChangeProductPrice(Product product, decimal newPrice)
    {
        ShopProductInfo? shopItem = _shopItems.FirstOrDefault(item => item.Product == product);
        if (shopItem == null)
        {
            throw new NoProductInTheShopException(this, product);
        }

        shopItem.Price = newPrice;
    }

    public bool IsOrderAvailableToBuy(List<OrderItem> order)
    {
        foreach (OrderItem orderItem in order)
        {
            ShopProductInfo? shopItem = _shopItems.FirstOrDefault(item => item.Product == orderItem.Product);
            if (shopItem == null || shopItem.Amount < orderItem.Amount)
            {
                return false;
            }
        }

        return true;
    }

    public decimal CountOrderCost(List<OrderItem> order)
    {
        decimal cost = 0;
        foreach (OrderItem orderItem in order)
        {
            ShopProductInfo? shopItem = _shopItems.FirstOrDefault(item => item.Product == orderItem.Product);
            if (shopItem == null || shopItem.Amount < orderItem.Amount)
            {
                throw new NotEnoughProductException(this, orderItem.Product);
            }

            cost += orderItem.Amount * shopItem.Price;
        }

        return cost;
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