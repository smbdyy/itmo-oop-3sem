using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Models;

public class VerifiedClientValidator : TransactionValidator
{
    private decimal _limit;

    public VerifiedClientValidator(decimal limit)
    {
        _limit = limit;
    }

    public override decimal Withdraw(IBankAccount account, decimal moneyAmount)
    {
        Validate(account, moneyAmount);
        return base.Withdraw(account, moneyAmount);
    }

    public override decimal Send(TransferTransaction transaction, decimal moneyAmount)
    {
        Validate(transaction.Sender, moneyAmount);
        return base.Send(transaction, moneyAmount);
    }

    private void Validate(IBankAccount account, decimal moneyAmount)
    {
        if (account.Client.Address is not null && account.Client.PassportNumber is not null) return;
        if (moneyAmount > _limit)
        {
            throw new NotImplementedException();
        }
    }
}