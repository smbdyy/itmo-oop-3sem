using Isu.Exceptions;

namespace Isu.Models;

public class CourseNumber
{
    public CourseNumber(AcademicDegree degree, int number)
    {
        if ((number is < 1 or > 4) || (degree == AcademicDegree.Master && number > 2))
        {
            throw new IncorrectCourseNumberException(degree, number);
        }

        this.Degree = degree;
        this.Number = number;
    }

    private AcademicDegree Degree { get; }
    private int Number { get; }
}