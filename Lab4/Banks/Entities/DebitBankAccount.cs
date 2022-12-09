using Banks.Interfaces;

namespace Banks.Entities;

public class DebitBankAccount : IBankAccount
{
    private IAccountState _state;
    private List<ITransaction> _transactions = new ();

    public DebitBankAccount(BankClient client, IAccountState state)
    {
        Client = client;
        _state = state;
    }

    public BankClient Client { get; }
    public decimal MoneyAmount { get; private set; }

    public decimal MinMoneyAmount => 0;

    public void SetState(IAccountState state)
    {
        _state = state;
    }

    public decimal Withdraw(decimal amount)
    {
        MoneyAmount = _state.Withdraw(MoneyAmount, amount);
        _transactions.Add(new WithdrawalTransaction(amount));
        return amount;
    }

    public void Replenish(decimal amount)
    {
        MoneyAmount = _state.Replenish(MoneyAmount, amount);
        _transactions.Add(new ReplenishmentTransaction(amount));
    }

    public void Send(decimal amount, IBankAccount recipient)
    {
        var transaction = new TransferTransaction(amount, this, recipient);
        MoneyAmount = _state.Send(transaction);
        _transactions.Add(transaction);
    }

    public void Receive(TransferTransaction transaction)
    {
        var receiveTransaction = new ReceiveTransferTransaction(transaction);
        MoneyAmount = _state.Replenish(MoneyAmount, transaction.Amount);
        _transactions.Add(receiveTransaction);
    }

    public void Undo(Guid transactionId)
    {
        ITransaction? transaction = _transactions.FirstOrDefault(t => t.Id == transactionId);
        if (transaction is null)
        {
            throw new NotImplementedException();
        }

        MoneyAmount = transaction.GetUndoResult(MoneyAmount);
        _transactions.Remove(transaction);
    }
}