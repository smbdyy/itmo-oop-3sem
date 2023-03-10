using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Models;

public class DepositPercentInfo
{
    public DepositPercentInfo(MoneyAmount startAmount, MoneyAmount percent)
    {
        if (startAmount < 0)
        {
            throw ArgumentException.InappropriateNegativeNumber(startAmount);
        }

        StartAmount = startAmount;
        Percent = percent;
    }

    public MoneyAmount StartAmount { get; }
    public MoneyAmount Percent { get; }
}