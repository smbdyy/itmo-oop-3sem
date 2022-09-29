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
        set
        {
            if (value < 0)
            {
                throw new IncorrectMoneyAmountException(value);
            }

            _money = value;
        }
    }
}