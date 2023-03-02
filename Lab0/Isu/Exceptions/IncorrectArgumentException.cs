using Isu.Models;

namespace Isu.Exceptions;

public class IncorrectArgumentException : IsuException
{
    public IncorrectArgumentException(string? message)
        : base(message) { }

    public static IncorrectArgumentException CourseNumber(AcademicDegree degree, int number)
    {
        if (number < 1)
        {
            return new IncorrectArgumentException($"course number cannot be less than 1 ({number} is given)");
        }

        string message = degree switch
        {
            AcademicDegree.Bachelor => "bachelor's max course number is 4 ({number} is given)",
            AcademicDegree.Doctor => "doctor's max course number is 4 ({number} is given)",
            AcademicDegree.Master => "master's max course number is 2 ({number} is given)",
            _ => string.Empty,
        };

        return new IncorrectArgumentException(message);
    }

    public static IncorrectArgumentException GroupNumber(int groupNumber)
    {
        return new IncorrectArgumentException($"group number must be >= 0 and <= 99 ({groupNumber} is given)");
    }

    public static IncorrectArgumentException MaxStudentsAmount(int value)
    {
        return new IncorrectArgumentException($"max students amount must be at least 1 ({value} is given)");
    }

    public static IncorrectArgumentException Name(string name, string surname)
    {
        return new IncorrectArgumentException($"incorrect name: {name} {surname}");
    }

    public static IncorrectArgumentException SpecialtyId(char specialtyId)
    {
        return new IncorrectArgumentException($"specialty id must be a capital letter ({specialtyId} is given)");
    }
}