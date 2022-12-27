using Banks.Entities;
using Banks.Interfaces;
using Banks.Tools.Exceptions;

namespace Banks.Builders;

public class DefaultBankBuilder : BankBuilder
{
    public override IBank Build()
    {
        return new Bank(
            Name,
            DepositAccountTerm,
            CreditAccountCommission,
            CreditAccountLimit,
            MaxUnverifiedClientWithdrawal,
            NotificationBuilder);
    }
}