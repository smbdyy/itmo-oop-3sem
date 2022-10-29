using Shops.Entities;

namespace Shops.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() { }

    public NotFoundException(string message)
        : base(message) { }

    public static NotFoundException ProductIsNotFound(Guid id)
    {
        return new NotFoundException($"product with id {id} is not found");
    }

    public static NotFoundException ShopIsNotFound(Guid id)
    {
        return new NotFoundException($"shop with id {id} is not found");
    }

    public static NotFoundException NoProductInTheShop(Product product, Shop shop)
    {
        return new NotFoundException($"there is no product {product.Name} in shop {shop.Name}");
    }
}