using Banks.Entities;
using Banks.Interfaces;
using Banks.Tools.Exceptions;

namespace Banks.Builders;

public class DefaultBankBuilder : BankBuilder
{
    public override IBank Build()
    {
        if (Name is null)
        {
            throw new RequiredFieldInBuilderIsNullException();
        }

        return new Bank(
            Name,
            DepositAccountTerm,
            CreditAccountCommission,
            CreditAccountLimit,
            MaxUnverifiedClientWithdrawal);
    }
}