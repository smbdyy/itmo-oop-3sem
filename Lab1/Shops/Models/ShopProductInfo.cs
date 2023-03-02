using Shops.Entities;
using Shops.Exceptions;

namespace Shops.Models;

public class ShopProductInfo
{
    public ShopProductInfo(int amount, decimal price)
    {
        if (amount < 0)
        {
            throw IncorrectArgumentException.IncorrectProductAmount(amount);
        }

        if (price <= 0)
        {
            throw IncorrectArgumentException.IncorrectPrice(price);
        }

        Amount = amount;
        Price = price;
    }

    public int Amount { get; }
    public decimal Price { get; }

    public static ShopProductInfo FromShopItem(ShopItem item)
        => new ShopProductInfo(item.Amount, item.Price);
}