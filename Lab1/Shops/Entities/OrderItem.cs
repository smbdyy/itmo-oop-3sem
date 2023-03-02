using Shops.Exceptions;

namespace Shops.Entities;

public class OrderItem
{
    public OrderItem(Product product, int amount = 1)
    {
        if (amount < 0)
        {
            throw IncorrectArgumentException.IncorrectProductAmount(amount);
        }

        Amount = amount;
        Product = product;
    }

    public Product Product { get; }
    public int Amount { get; }
}