using Banks.Entities;
using Banks.Interfaces;
using Banks.Tools.Exceptions;

namespace Banks.Builders;

public class DebitBankAccountBuilder : BankAccountBuilder
{
    public override IBankAccount Build()
    {
        return new DebitBankAccount(Client, Bank.UnverifiedClientWithdrawalLimit, Bank.CurrentDate);
    }
}