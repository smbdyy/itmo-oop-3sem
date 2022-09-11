using Isu.Models;

namespace Isu.Exceptions;

public class IncorrectCourseNumberException : Exception
{
    public IncorrectCourseNumberException() { }

    public IncorrectCourseNumberException(AcademicDegree degree, int number)
    {
        if (number < 1)
        {
            this.Message = $"course number cannot be less than 1 ({number} is given)";
        }

        this.Message = degree switch
        {
            AcademicDegree.Bachelor => "bachelor's max course number is 4 ({number} is given)",
            AcademicDegree.Doctor => "doctor's max course number is 4 ({number} is given)",
            AcademicDegree.Master => "master's max course number is 2 ({number} is given)",
            _ => string.Empty,
        };
    }

    public override string Message { get; } = string.Empty;
}