using System.Text.RegularExpressions;
using Isu.Exceptions;

namespace Isu.Entities;

public class Student
{
    public Student(Group group, string name, string surname)
    {
        Group = group;
        Name = name;
        Surname = surname;

        if (!IsFullNameCorrect())
        {
            throw new IncorrectStudentNameException(this);
        }
    }

    public Student(Group group, string name, string surname, string patronymic)
        : this(group, name, surname)
    {
        Patronymic = patronymic;

        if (!IsFullNameCorrect())
        {
            throw new IncorrectStudentNameException(this);
        }
    }

    public int Id { get; }
    public string Name { get; }
    public string Surname { get; }

    // Patronymic is nullable, because someone may not have it
    public string? Patronymic { get; }

    public Group Group { get; }

    public string GetNameAsString()
    {
        return $"{Surname} {Name} {Patronymic}";
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