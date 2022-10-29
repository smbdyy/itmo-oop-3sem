using Shops.Exceptions;

namespace Shops.Entities;

public class Person
{
    private decimal _money;

    public Person(string name, decimal money)
    {
        Name = name;
        Money = money;
    }

    public string Name { get; }

    public decimal Money
    {
        get => _money;
        private set
        {
            if (value < 0)
            {
                throw IncorrectArgumentException.IncorrectMoneyAmount(value);
            }

            _money = value;
        }
    }

    public void SubtractMoney(decimal amount)
    {
        if (amount < 0)
        {
            throw IncorrectArgumentException.IncorrectMoneyAmount(amount);
        }

        if (amount > Money)
        {
            throw NotEnoughException.NotEnoughMoney(this, amount);
        }

        Money -= amount;
    }

    public void GiveMoney(decimal amount)
    {
        if (amount < 0)
        {
            throw IncorrectArgumentException.IncorrectMoneyAmount(amount);
        }

        Money += amount;
    }
}