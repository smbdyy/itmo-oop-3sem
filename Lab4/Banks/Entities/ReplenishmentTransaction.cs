using Banks.Interfaces;

namespace Banks.Entities;

public class ReplenishmentTransaction : ITransaction
{
    public ReplenishmentTransaction(string name, decimal amount)
    {
        if (name == string.Empty)
        {
            throw new NotImplementedException();
        }

        Name = name;
        Amount = amount;
    }

    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }
    public decimal Amount { get; }

    public decimal GetUndoResult(decimal accountMoney)
    {
        return accountMoney - Amount;
    }
}