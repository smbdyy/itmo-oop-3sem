namespace Banks.Accounts.Builders;

public class CreditBankAccountBuilder : BankAccountBuilder
{
    public override IBankAccount Build()
    {
        return new CreditBankAccount(
            Client,
            Bank.CreditAccountLimit,
            Bank.CreditAccountCommission,
            Bank.UnverifiedClientWithdrawalLimit,
            Bank.CurrentDate);
    }
}