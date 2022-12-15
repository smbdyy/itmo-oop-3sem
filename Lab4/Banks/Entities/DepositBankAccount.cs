using Banks.Interfaces;

namespace Banks.Entities;

public class DepositBankAccount : IBankAccount
{
    private readonly List<ITransaction> _transactions = new ();
    private IAccountState _state;

    public DepositBankAccount(BankClient client, IAccountState state, decimal moneyAmount, decimal percent)
    {
        if (moneyAmount < 0)
        {
            throw new NotImplementedException();
        }

        if (percent < 0)
        {
            throw new NotImplementedException();
        }

        _state = state;
        Client = client;
        MoneyAmount = moneyAmount;
        Percent = percent;
    }

    public BankClient Client { get; }
    public decimal MoneyAmount { get; private set; }
    public decimal Percent { get; }
    public DateOnly CreationDate { get; } = DateOnly.FromDateTime(DateTime.Now);

    public void SetState(IAccountState state)
    {
        _state = state;
    }

    public void Withdraw(decimal amount)
    {
        if (amount > MoneyAmount)
        {
            throw new NotImplementedException();
        }

        MoneyAmount = _state.Withdraw(MoneyAmount, amount);
        _transactions.Add(new WithdrawalTransaction(amount, 0));
    }

    public void Replenish(decimal amount)
    {
        MoneyAmount = _state.Replenish(MoneyAmount, amount);
        _transactions.Add(new ReplenishmentTransaction(amount, 0));
    }

    public void Send(decimal amount, IBankAccount recipient)
    {
        if (amount > MoneyAmount)
        {
            throw new NotImplementedException();
        }

        var transaction = new TransferTransaction(amount, 0, this, recipient);
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
}