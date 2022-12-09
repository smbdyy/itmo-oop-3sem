namespace Banks.Interfaces;

public interface ITransaction
{
    public Guid Id { get; }
    public string Name { get; }
    public decimal Amount { get; }
    public decimal GetUndoResult(decimal accountMoney);
}