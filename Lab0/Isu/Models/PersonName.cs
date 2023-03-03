using System.Text.RegularExpressions;
using Isu.Exceptions;

namespace Isu.Models;

public class PersonName
{
    public PersonName(string name, string surname)
    {
        ThrowIfNameIncorrect(name, surname);
        Name = name;
        Surname = surname;
    }

    public string Name { get; }
    public string Surname { get; }

    public string AsString()
    {
        return $"{Name} {Surname}";
    }

    private static void ThrowIfNameIncorrect(string name, string surname)
    {
        const string cyrillicNamePattern = @"^[А-Я][а-я]*$";
        const string latinNamePattern = @"^[A-Z][a-z]*$";

        if (Regex.IsMatch(name, cyrillicNamePattern) && Regex.IsMatch(surname, cyrillicNamePattern))
        {
            return;
        }

        if (Regex.IsMatch(name, latinNamePattern) && Regex.IsMatch(surname, latinNamePattern))
        {
            return;
        }

        throw IncorrectArgumentException.Name(name, surname);
    }
}