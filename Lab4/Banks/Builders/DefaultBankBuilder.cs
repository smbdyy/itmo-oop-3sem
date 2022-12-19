using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Builders;

public class DefaultBankBuilder : BankBuilder
{
    public override IBank Build()
    {
        if (Name is null)
        {
            throw new NotImplementedException();
        }

        return new Bank(
            Name,
            DepositAccountTerm,
            CreditAccountCommission,
            CreditAccountLimit,
            MaxUnverifiedClientWithdrawal);
    }
}