using Banks.Interfaces;

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
    public decimal Amount { get; }
    public decimal Commission { get; }
    public string Description { get; }
}