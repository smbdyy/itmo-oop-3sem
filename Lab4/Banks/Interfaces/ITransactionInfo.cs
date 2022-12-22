namespace Banks.Interfaces;

public interface ITransactionInfo
{
    public Guid TransactionId { get; }
    public decimal Amount { get; }
    public decimal Commission { get; }
}