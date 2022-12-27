using Banks.Models;
using Banks.Tools.Exceptions;

namespace Banks.Builders;

public class UpdateNotificationBuilder : BankNotificationBuilder
{
    public override string GetNotificationMessage()
    {
        if (Bank is null)
        {
            throw new RequiredFieldInBuilderIsNullException();
        }

        string message = @$"Our terms has been updated. Current terms: 
                            Deposit account term: {Bank.DepositAccountTerm.Value},
                            Credit account commission: {Bank.CreditAccountCommission.Value},
                            Credit account limit: {Bank.CreditAccountLimit.Value},
                            Unverified client withdrawal limit: {Bank.UnverifiedClientWithdrawalLimit.Value}.
                            Deposit account percents:";
        foreach (StartAmountPercentPair pair in Bank.StartAmountPercentPairs)
        {
            message += Environment.NewLine + $"From {pair.StartAmount}: {pair.Percent}";
        }

        return message;
    }
}