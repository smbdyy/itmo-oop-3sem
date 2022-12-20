using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Models;

public class PassportNumber
{
    public PassportNumber(string series, string number)
    {
        if (series.Length != 4 || series.Any(symbol => !char.IsDigit(symbol)) ||
            number.Length != 6 || number.Any(symbol => !char.IsDigit(symbol)))
        {
            throw ArgumentException.IncorrectPassportNumber(series, number);
        }

        Series = series;
        Number = number;
    }

    public string Series { get; }
    public string Number { get; }

    public static PassportNumber FromString(string passportNumber)
    {
        string[] asArray = passportNumber.Split(' ');
        if (asArray.Length != 2)
        {
            throw ArgumentException.IncorrectPassportNumberString(passportNumber);
        }

        return new PassportNumber(asArray[0], asArray[1]);
    }

    public string AsString()
    {
        return $"{Series} {Number}";
    }
}