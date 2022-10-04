﻿using Shops.Exceptions;

namespace Shops.Entities;

public class ShopItem
{
    private int _amount;
    private decimal _price;

    public ShopItem(Product product, int amount, decimal price)
    {
        Product = product;
        Amount = amount;
        Price = price;
    }

    public Product Product { get; }

    public decimal Price
    {
        get => _price;
        set
        {
            if (value <= 0)
            {
                throw IncorrectArgumentException.IncorrectPrice(value);
            }

            _price = value;
        }
    }

    private int Amount
    {
        get => _amount;
        set
        {
            if (value < 0)
            {
                throw IncorrectArgumentException.IncorrectProductAmount(value);
            }

            _amount = value;
        }
    }

    public void SubtractAmount(int amount)
    {
        if (amount < 0)
        {
            throw IncorrectArgumentException.IncorrectProductAmount(amount);
        }

        if (amount > Amount)
        {
            throw NotEnoughException.NotEnoughProduct(this.Product);
        }

        Amount -= amount;
    }
}