namespace Shops.Exceptions;

public class ShopIsNotFoundException : Exception
{
    public ShopIsNotFoundException(Guid id)
    {
        Message = $"shop with id {id} is not found";
    }

    public override string Message { get; } = "shop is not found";
}