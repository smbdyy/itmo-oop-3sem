using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber
{
    // there are 3 academic degrees: bachelor (4 courses), master (2 courses) and doctor (4 courses)
    public CourseNumber(AcademicDegree degree, int number)
    {
        if (number is < 1 or > 4 || (degree == AcademicDegree.Master && number > 2))
        {
            throw IncorrectArgumentException.CourseNumber(degree, number);
        }

        Degree = degree;
        Number = number;
    }

    public AcademicDegree Degree { get; }
    public int Number { get; }
}