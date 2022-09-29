using Shops.Entities;

namespace Shops.Exceptions;

public class NotEnoughMoneyException : Exception
{
    public NotEnoughMoneyException(Person person, decimal moneyNeeded)
    {
        Message = $"person {person.Name} has {person.Money} money, {moneyNeeded} needed";
    }

    public override string Message { get; } = "person has not enough money";
}