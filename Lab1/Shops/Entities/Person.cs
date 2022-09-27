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
        private set
        {
            if (value < 0)
            {
                throw new IncorrectMoneyAmountException(value);
            }

            _money = value;
        }
    }

    public void SubtractMoney(int moneyAmount)
    {
        if (moneyAmount < Money)
        {
            throw new NotEnoughMoneyException(this, moneyAmount);
        }

        if (moneyAmount >= Money)
        {
            Money -= moneyAmount;
        }
    }
}