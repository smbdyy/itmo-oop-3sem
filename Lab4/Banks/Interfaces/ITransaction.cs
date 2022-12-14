namespace Banks.Interfaces;

public interface ITransaction
{
    public Guid Id { get; }
    public decimal Amount { get; }
    public decimal Commission { get; }
    public decimal GetUndoResult(decimal accountMoney);
}