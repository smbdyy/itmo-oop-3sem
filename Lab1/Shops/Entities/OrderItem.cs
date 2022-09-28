using Shops.Exceptions;

namespace Shops.Entities;

public class OrderItem
{
    public OrderItem(Product product, int amount = 1)
    {
        Product = product;

        if (amount < 0)
        {
            throw new IncorrectProductAmountException();
        }

        Amount = amount;
    }

    public Product Product { get; }
    public int Amount { get; }
}