using Banks.Models;

namespace Banks.Transactions.Info;

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