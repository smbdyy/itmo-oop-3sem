using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class Lesson
{
    public Lesson(Teacher teacher, int timeId, DayOfWeek week, Classroom classroom, string subjectName)
    {
        if (timeId < 1)
        {
            throw new NotImplementedException();
        }

        if (subjectName == string.Empty)
        {
            throw new NotImplementedException();
        }

        Teacher = teacher;
        TimeId = timeId;
        Week = week;
        Classroom = classroom;
        SubjectName = subjectName;
        Id = Guid.NewGuid();
    }

    public Teacher Teacher { get; }
    public int TimeId { get; }
    public DayOfWeek Week { get; }
    public Classroom Classroom { get; }
    public string SubjectName { get; }
    public Guid Id { get; }
}