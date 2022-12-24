using Banks.Models;

namespace Banks.Interfaces;

public interface ITransactionInfo
{
    public Guid TransactionId { get; }
    public MoneyAmount Amount { get; }
    public MoneyAmount Commission { get; }
    public string Description { get; }
}