using Banks.Interfaces;

namespace Banks.Tools.Exceptions;

public class TransactionValidationException : Exception
{
    public TransactionValidationException(string message)
        : base(message) { }

    public static TransactionValidationException NotEnoughMoney(IBankAccount account, decimal requiredAmount)
    {
        return new TransactionValidationException($"account only has {account.MoneyAmount}, but {requiredAmount} needed");
    }

    public static TransactionValidationException DepositAccountNotExpired(IBankAccount account, int daysToExpire)
    {
        return new TransactionValidationException(
            $"this deposit account can only withdraw money after {daysToExpire} days from {account.CreationDate.ToString()}");
    }

    public static TransactionValidationException UnverifiedClientWithdrawalLimitExceeded(
        IBankAccount account,
        decimal requiredAmount)
    {
        return new TransactionValidationException(
            $"unverified client {account.Client.Name.AsString} cannot withdraw {requiredAmount}");
    }

    public static TransactionValidationException RecipientIsSender()
    {
        return new TransactionValidationException("recipient cannot be the same account as sender");
    }
}