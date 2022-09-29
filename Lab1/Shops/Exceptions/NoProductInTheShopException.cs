using Shops.Entities;

namespace Shops.Exceptions;

public class NoProductInTheShopException : Exception
{
    public NoProductInTheShopException(Shop shop, Product product)
    {
        Message = $"there is no product {product.Name} in the shop {shop.Name}";
    }

    public override string Message { get; } = "there is no requested product in the shop";
}