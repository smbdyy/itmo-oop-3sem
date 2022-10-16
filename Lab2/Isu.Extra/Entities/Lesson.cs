using Isu.Entities;
using Isu.Extra.Models;

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

public class OgnpLesson
{
    public OgnpLesson(Lesson lesson, OgnpStream stream)
    {
        Lesson = lesson;
        Stream = stream;
    }

    public Lesson Lesson { get; }
    public OgnpStream Stream { get; }
}

public class GroupLesson
{
    public GroupLesson(Lesson lesson, Group group)
    {
        Lesson = lesson;
        Group = group;
    }

    public Lesson Lesson { get; }
    public Group Group { get; }
}