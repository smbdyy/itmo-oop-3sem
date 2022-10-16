using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Lesson
{
    public Lesson(Teacher teacher, LessonTime time, Classroom classroom, string subjectName)
    {
        if (subjectName == string.Empty)
        {
            throw new NotImplementedException();
        }

        Teacher = teacher;
        Time = time;
        Classroom = classroom;
        SubjectName = subjectName;
        Id = Guid.NewGuid();
    }

    public Teacher Teacher { get; }

    public LessonTime Time { get; }
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