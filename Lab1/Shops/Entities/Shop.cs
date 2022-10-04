using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private readonly List<ShopItem> _shopItems = new ();
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

    public bool IsProductInShop(Product product)
    {
        return _shopItems.Any(item => item.Product == product);
    }

    public ShopItem GetProductInfo(Product product)
    {
        ShopItem item = _shopItems.First(item => item.Product == product);
        if (item == null)
        {
            throw NotFoundException.NoProductInTheShop(product, this);
        }

        return item;
    }

    public void Buy(Person person, List<OrderItem> order)
    {
        foreach (OrderItem orderItem in order)
        {
            ShopItem? shopItem = _shopItems.FirstOrDefault(item => item.Product == orderItem.Product);
            if (shopItem == null)
            {
                throw NotFoundException.NoProductInTheShop(orderItem.Product, this);
            }

            if (shopItem.Amount < orderItem.Amount)
            {
                throw NotEnoughException.NotEnoughProduct(orderItem.Product);
            }
        }

        decimal cost = CountOrderCost(order);
        if (cost > person.Money)
        {
            throw NotEnoughException.NotEnoughMoney(person, cost);
        }

        person.SubtractMoney(cost);

        foreach (OrderItem orderItem in order)
        {
            ShopItem shopItem = _shopItems.First(item => item.Product == orderItem.Product);
            shopItem.SubtractAmount(orderItem.Amount);
        }
    }

    public void Supply(List<ShopItem> supplement)
    {
        foreach (ShopItem supplementItem in supplement)
        {
            ShopItem? shopItem = _shopItems.FirstOrDefault(item => item.Product == supplementItem.Product);
            if (shopItem == null)
            {
                _shopItems.Add(supplementItem);
            }
            else
            {
                shopItem.AddAmount(supplementItem.Amount);
                shopItem.Price = supplementItem.Price;
            }
        }
    }

    public void ChangeProductPrice(Product product, decimal newPrice)
    {
        ShopItem? shopItem = _shopItems.FirstOrDefault(item => item.Product == product);
        if (shopItem == null)
        {
            throw NotFoundException.NoProductInTheShop(product, this);
        }

        shopItem.Price = newPrice;
    }

    public bool IsOrderAvailableToBuy(List<OrderItem> order)
    {
        foreach (OrderItem orderItem in order)
        {
            ShopItem? shopItem = _shopItems.FirstOrDefault(item => item.Product == orderItem.Product);
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
            ShopItem? shopItem = _shopItems.FirstOrDefault(item => item.Product == orderItem.Product);
            if (shopItem == null)
            {
                throw NotFoundException.NoProductInTheShop(orderItem.Product, this);
            }

            if (shopItem.Amount < orderItem.Amount)
            {
                throw NotEnoughException.NotEnoughProduct(orderItem.Product);
            }

            cost += orderItem.Amount * shopItem.Price;
        }

        return cost;
    }

    private static string Validate(string value)
    {
        if (value == string.Empty)
        {
            throw IncorrectArgumentException.EmptyName();
        }

        return value;
    }
}