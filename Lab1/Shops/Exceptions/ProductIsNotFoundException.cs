namespace Shops.Exceptions;

public class ProductIsNotFoundException : Exception
{
    public ProductIsNotFoundException(Guid id)
    {
        Message = $"product with id {id} is not found";
    }

    public override string Message { get; } = "product is not found";
}