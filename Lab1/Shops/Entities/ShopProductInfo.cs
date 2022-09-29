using Shops.Exceptions;

namespace Shops.Entities;

public class ShopProductInfo
{
    private int _amount;
    private decimal _price;

    public ShopProductInfo(Product product, int amount, decimal price)
    {
        Product = product;
        Amount = amount;
        Price = price;
    }

    public Product Product { get; }

    public int Amount
    {
        get => _amount;
        set
        {
            if (value < 0)
            {
                throw new IncorrectProductAmountException();
            }

            _amount = value;
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (value <= 0)
            {
                throw new IncorrectPriceException();
            }

            _price = value;
        }
    }
}