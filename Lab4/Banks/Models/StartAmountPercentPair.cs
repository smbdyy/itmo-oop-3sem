using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Models;

public class StartAmountPercentPair
{
    public StartAmountPercentPair(decimal startAmount, decimal percent)
    {
        if (startAmount < 0)
        {
            throw ArgumentException.InappropriateNegativeNumber(startAmount);
        }

        if (percent < 0)
        {
            throw ArgumentException.InappropriateNegativeNumber(startAmount);
        }

        StartAmount = startAmount;
        Percent = percent;
    }

    public decimal StartAmount { get; }
    public decimal Percent { get; }
}