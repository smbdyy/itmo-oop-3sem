using System.Text.RegularExpressions;
using Isu.Entities;
using Isu.Exceptions;

namespace Isu.Models;

// the purpose of creating separate class for student name
// is validation and separating it to name, surname and patronymic
public class StudentName
{
    public StudentName(string name, string surname)
    {
        Name = name;
        Surname = surname;

        if (!IsFullNameCorrect())
        {
            throw new IncorrectStudentNameException(this);
        }
    }

    public StudentName(string name, string surname, string patronymic)
        : this(name, surname)
    {
        Patronymic = patronymic;

        if (!IsFullNameCorrect())
        {
            throw new IncorrectStudentNameException(this);
        }
    }

    public string Name { get; }
    public string Surname { get; }

    // Patronymic is nullable, because someone may not have it
    public string? Patronymic { get; } = null;

    public string AsString()
    {
        return Patronymic is null ? $"{Surname} {Name}" : $"{Surname} {Name} {Patronymic}";
    }

    private bool IsFullNameCorrect()
    {
        // name, surname and patronymic must start with a capital letter, next letters must be from the same alphabet
        const string cyrillicNamePattern = @"^[А-Я][а-я]*$";
        const string latinNamePattern = @"^[A-Z][a-z]*$";

        if (Regex.IsMatch(Name, cyrillicNamePattern))
        {
            return Regex.IsMatch(Surname, cyrillicNamePattern) && (Patronymic is null || Regex.IsMatch(Patronymic, cyrillicNamePattern));
        }

        if (Regex.IsMatch(Name, latinNamePattern))
        {
            return Regex.IsMatch(Surname, latinNamePattern) && (Patronymic is null || Regex.IsMatch(Patronymic, latinNamePattern));
        }

        return false;
    }
}