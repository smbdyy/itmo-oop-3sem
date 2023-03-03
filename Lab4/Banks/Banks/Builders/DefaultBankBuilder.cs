namespace Banks.Banks.Builders;

public class DefaultBankBuilder : BankBuilder
{
    public override IBank Build()
    {
        return new Bank(
            Name,
            DepositAccountTerm,
            CreditAccountCommission,
            CreditAccountLimit,
            UnverifiedClientWithdrawalLimit,
            NotificationBuilder);
    }
}