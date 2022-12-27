using Banks.Interfaces;
using Banks.Models;

namespace Banks.Entities;

public class ReplenishmentTransactionInfo : ITransactionInfo
{
    public ReplenishmentTransactionInfo(ReplenishmentTransaction transaction)
    {
        TransactionId = transaction.Id;
        Amount = transaction.Amount;
        Commission = transaction.Commission;
        Description = $"replenish {Amount}, commission {Commission}";
    }

    public Guid TransactionId { get; }
    public MoneyAmount Amount { get; }
    public MoneyAmount Commission { get; }
    public string Description { get; }
}