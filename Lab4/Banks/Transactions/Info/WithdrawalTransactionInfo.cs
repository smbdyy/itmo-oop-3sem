using Banks.Models;

namespace Banks.Transactions.Info;

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
    public MoneyAmount Amount { get; }
    public MoneyAmount Commission { get; }
    public string Description { get; }
}