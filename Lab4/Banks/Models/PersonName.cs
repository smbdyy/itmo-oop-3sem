namespace Banks.Models;

public class PersonName
{
    public PersonName(string name, string surname)
    {
        Name = Validate(name);
        Surname = Validate(surname);
    }

    public string Name { get; }
    public string Surname { get; }

    public string AsString => $"{Name} {Surname}";

    public static PersonName FromString(string name)
    {
        string[] asArray = name.Split(' ');
        if (asArray.Length != 2)
        {
            throw new NotImplementedException();
        }

        return new PersonName(asArray[0], asArray[1]);
    }

    private static string Validate(string value)
    {
        if (value == string.Empty || !value.All(IsLatinLetter) || !char.IsUpper(value[0]))
        {
            throw new NotImplementedException();
        }

        return value;
    }

    private static bool IsLatinLetter(char symbol)
    {
        return symbol is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
    }
}