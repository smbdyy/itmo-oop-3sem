using Shops.Entities;

namespace Shops.Exceptions;

public class NotEnoughException : Exception
{
    public NotEnoughException() { }

    public NotEnoughException(string message)
        : base(message) { }

    public static NotEnoughException NotEnoughProduct(Product product)
    {
        return new NotEnoughException($"not enough product {product.Name} available");
    }

    public static NotEnoughException NotEnoughMoney(Person person, decimal moneyNeeded)
    {
        return new NotEnoughException($"person {person.Name} only has {person.Money} money, {moneyNeeded} needed");
    }
}