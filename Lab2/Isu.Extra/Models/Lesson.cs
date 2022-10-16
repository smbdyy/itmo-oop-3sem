using Isu.Models;

namespace Isu.Extra.Models;

public class Lesson
{
    public Lesson(PersonName teacher, int timeId, DayOfWeek week, Classroom classroom)
    {
        if (timeId < 1)
        {
            throw new NotImplementedException();
        }

        Teacher = teacher;
        TimeId = timeId;
        Week = week;
        Classroom = classroom;
    }

    public PersonName Teacher { get; }
    public int TimeId { get; }
    public DayOfWeek Week { get; }
    public Classroom Classroom { get; }
}