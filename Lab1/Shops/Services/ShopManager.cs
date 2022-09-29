using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Services;

public class ShopManager
{
    private List<Shop> _shops = new ();
    private List<Product> _products = new ();

    public Product RegisterProduct(string name)
    {
        var product = new Product(name);
        _products.Add(product);
        return product;
    }

    public Shop RegisterShop(string name, string address)
    {
        var shop = new Shop(name, address);
        _shops.Add(shop);
        return shop;
    }

    public Product? FindProductById(Guid id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }

    public Product GetProductById(Guid id)
    {
        if (_products.All(p => p.Id != id))
        {
            throw new ProductIsNotFoundException(id);
        }

        return _products.First(p => p.Id == id);
    }

    public List<Product> FindProductsByName(string name)
    {
        return _products.Where(p => p.Name == name).ToList();
    }

    public Shop? FindShopById(Guid id)
    {
        return _shops.FirstOrDefault(s => s.Id == id);
    }

    public Shop GetShopById(Guid id)
    {
        if (_shops.All(s => s.Id != id))
        {
            throw new ShopIsNotFoundException(id);
        }

        return _shops.First(s => s.Id == id);
    }

    public List<Shop> FindShopsByName(string name)
    {
        return _shops.Where(s => s.Name == name).ToList();
    }

    public Shop? FindShopWithLowestCost(List<OrderItem> order)
    {
        var availableShops = _shops.Where(s => s.IsOrderAvailableToBuy(order)).ToList();
        if (!availableShops.Any())
        {
            return null;
        }

        return availableShops.MinBy(s => s.CountOrderCost(order));
    }
}