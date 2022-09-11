namespace Isu.Models;

public enum AcademicDegree
{
    Bachelor,
    Master,
    Doctor,
}

public class CourseNumber
{
    public CourseNumber(AcademicDegree degree, int number)
    {
        if (number is < 1 or > 4)
        {
            // throw
        }
        else if (degree == AcademicDegree.Master && number > 2)
        {
            // throw
        }

        this.Degree = degree;
        this.Number = number;
    }

    private AcademicDegree Degree { get; }
    private int Number { get; }
}