namespace Banks.Accounts.Builders;

public class DebitBankAccountBuilder : BankAccountBuilder
{
    public override IBankAccount Build()
    {
        return new DebitBankAccount(Client, Bank.UnverifiedClientWithdrawalLimit, Bank.CurrentDate);
    }
}