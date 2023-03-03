using Banks.Models;

namespace Banks.Transactions.Info;

public interface ITransactionInfo
{
    public Guid TransactionId { get; }
    public MoneyAmount Amount { get; }
    public MoneyAmount Commission { get; }
    public string Description { get; }
}