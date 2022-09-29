using Shops.Entities;

namespace Shops.Exceptions;

public class NotEnoughProductException : Exception
{
    public NotEnoughProductException(Shop shop, Product product)
    {
        Message = $"not enough product {product.Name} in shop {shop.Name}";
    }

    public override string Message { get; } = "not enough product to buy";
}