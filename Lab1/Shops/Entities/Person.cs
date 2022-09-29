using Shops.Exceptions;

namespace Shops.Entities;

public class Person
{
    private int _money;

    public Person(string name, int money)
    {
        Name = name;
        Money = money;
    }

    public string Name { get; }

    public int Money
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