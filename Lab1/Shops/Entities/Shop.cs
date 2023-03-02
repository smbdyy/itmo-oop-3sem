using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private readonly List<ShopItem> _shopItems = new ();
    private string _name;
    private string _address;

    public Shop(Guid id, string name, string address)
    {
        _name = Validate(name);
        _address = Validate(address);
        Id = id;
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

    public bool IsProductAvailable(Product product)
    {
        return _shopItems.Any(item => item.Product == product);
    }

    public ShopProductInfo GetProductInfo(Product product)
    {
        ShopItem item = _shopItems.First(item => item.Product == product);
        if (item == null)
        {
            throw NotFoundException.NoProductInTheShop(product, this);
        }

        return ShopProductInfo.FromShopItem(item);
    }

    public void Buy(Person person, IEnumerable<OrderItem> order)
    {
        IEnumerable<OrderItem> orderItems = order as OrderItem[] ?? order.ToArray();
        ThrowIfOrderUnavailable(orderItems);

        decimal cost = CountOrderCost(orderItems);
        if (cost > person.Money)
        {
            throw NotEnoughException.NotEnoughMoney(person, cost);
        }

        person.SubtractMoney(cost);

        foreach (OrderItem orderItem in orderItems)
        {
            ShopItem shopItem = _shopItems.First(item => item.Product == orderItem.Product);
            shopItem.SubtractAmount(orderItem.Amount);
        }
    }

    public void Supply(IEnumerable<ShopItem> supplement)
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

    public bool IsOrderAvailableToBuy(IEnumerable<OrderItem> order)
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

    public decimal CountOrderCost(IEnumerable<OrderItem> order)
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

    private void ThrowIfOrderUnavailable(IEnumerable<OrderItem> order)
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
    }
}