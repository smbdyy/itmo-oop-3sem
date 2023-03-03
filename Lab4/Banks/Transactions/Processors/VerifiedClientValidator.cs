using Banks.Accounts;
using Banks.Models;
using Banks.Tools.Exceptions;

namespace Banks.Transactions.Processors;

public class VerifiedClientValidator : TransactionProcessor
{
    private MoneyAmount _limit;

    public VerifiedClientValidator(MoneyAmount limit)
    {
        _limit = limit;
    }

    public override decimal Withdraw(IBankAccount account, MoneyAmount moneyAmount)
    {
        Validate(account, moneyAmount);
        return base.Withdraw(account, moneyAmount);
    }

    public override decimal Send(TransferTransaction transaction)
    {
        Validate(transaction.Sender, transaction.Amount + transaction.Commission);
        return base.Send(transaction);
    }

    private void Validate(IBankAccount account, MoneyAmount moneyAmount)
    {
        if (account.Client.Address is not null && account.Client.PassportNumber is not null) return;
        if (moneyAmount > _limit)
        {
            throw TransactionValidationException.UnverifiedClientWithdrawalLimitExceeded(account, moneyAmount);
        }
    }
}