using Shops.Entities;

namespace Shops.Exceptions;

public class NotEnoughMoneyException : Exception
{
    public NotEnoughMoneyException(Person person, int moneyNeeded)
    {
        Message = $"person {person.Name} only has {person.Money} money ({moneyNeeded} needed)";
    }

    public override string Message { get; } = "person has not enough money";
}