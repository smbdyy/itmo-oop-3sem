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