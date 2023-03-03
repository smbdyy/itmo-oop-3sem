using Banks.Models;

namespace Banks.Transactions;

public interface ITransaction
{
    public Guid Id { get; }
    public MoneyAmount Amount { get; }
    public MoneyAmount Commission { get; }
    public decimal GetUndoResult(decimal accountMoney);
}