using Shops.Exceptions;

namespace Shops.Entities;

public class ShopProductInfo
{
    private int _amount;
    private int _price;

    public ShopProductInfo(Product product, int amount, int price)
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

    public int Price
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