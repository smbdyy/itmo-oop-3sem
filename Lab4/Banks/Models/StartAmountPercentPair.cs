namespace Banks.Models;

public class StartAmountPercentPair
{
    public StartAmountPercentPair(decimal startAmount, decimal percent)
    {
        if (startAmount < 0 || percent < 0)
        {
            throw new NotImplementedException();
        }

        StartAmount = startAmount;
        Percent = percent;
    }

    public decimal StartAmount { get; }
    public decimal Percent { get; }
}