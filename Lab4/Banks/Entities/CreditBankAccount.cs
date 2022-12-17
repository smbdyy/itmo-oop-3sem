using Banks.Interfaces;

namespace Banks.Entities;

public class CreditBankAccount : IBankAccount
{
    private readonly List<ITransaction> _transactions = new ();
    private IAccountState _state;

    public CreditBankAccount(BankClient client, IAccountState state, decimal limit, decimal commission)
    {
        if (limit > 0)
        {
            throw new NotImplementedException();
        }

        if (commission < 0)
        {
            throw new NotImplementedException();
        }

        Client = client;
        _state = state;
        Limit = limit;
        Commission = commission;
    }

    public BankClient Client { get; }
    public decimal MoneyAmount { get; private set; }
    public decimal Limit { get; }
    public decimal Commission { get; }
    public DateOnly CreationDate { get; } = DateOnly.FromDateTime(DateTime.Now);
    public DateOnly CurrentDate { get; } = DateOnly.FromDateTime(DateTime.Now);

    public void SetState(IAccountState state)
    {
        _state = state;
    }

    public void Withdraw(decimal amount)
    {
        if (MoneyAmount - amount - Commission < Limit)
        {
            throw new NotImplementedException();
        }

        MoneyAmount = _state.Withdraw(MoneyAmount, amount + Commission);
        _transactions.Add(new WithdrawalTransaction(amount, Commission));
    }

    public void Replenish(decimal amount)
    {
        MoneyAmount = _state.Replenish(MoneyAmount, amount);
        _transactions.Add(new ReplenishmentTransaction(amount, 0));
    }

    public void Send(decimal amount, IBankAccount recipient)
    {
        if (amount + Commission < MoneyAmount)
        {
            throw new NotImplementedException();
        }

        var transaction = new TransferTransaction(amount, Commission, this, recipient);
        MoneyAmount = _state.Send(transaction);
        _transactions.Add(transaction);
    }

    public void Receive(TransferTransaction transaction)
    {
        var receiveTransaction = new ReceiveTransferTransaction(transaction, 0);
        MoneyAmount = _state.Replenish(MoneyAmount, transaction.Amount);
        _transactions.Add(receiveTransaction);
    }

    public void Undo(Guid transactionId)
    {
        ITransaction? transaction = FindTransaction(transactionId);
        if (transaction is null)
        {
            throw new NotImplementedException();
        }

        MoneyAmount = transaction.GetUndoResult(MoneyAmount);
        _transactions.Remove(transaction);
    }

    public ITransaction? FindTransaction(Guid id)
    {
        return _transactions.FirstOrDefault(t => t.Id == id);
    }

    public void NotifyNextDay()
    {
        CurrentDate.AddDays(1);
    }
}