using Banks.Interfaces;

namespace Banks.Entities;

public class WithdrawalTransactionInfo : ITransactionInfo
{
    public WithdrawalTransactionInfo(WithdrawalTransaction transaction)
    {
        TransactionId = transaction.Id;
        Amount = transaction.Amount;
        Commission = transaction.Commission;
        Description = $"withdraw {Amount}, commission {Commission}";
    }

    public Guid TransactionId { get; }
    public decimal Amount { get; }
    public decimal Commission { get; }
    public string Description { get; }
}