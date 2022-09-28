using Shops.Entities;

namespace Shops.Exceptions;

public class NotEnoughProductException : Exception
{
    public NotEnoughProductException() { }

    // TODO shop
    public NotEnoughProductException(Product product, int productNeeded)
    {
        Message = $"pcs of product {product.Name} in shop, {productNeeded} needed";
    }

    public override string Message { get; } = "not enough product to buy";
}