using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public abstract class Lesson
{
    public Lesson(Guid id, Teacher teacher, LessonTime time, Classroom classroom, string subjectName)
    {
        if (subjectName == string.Empty)
        {
            throw IncorrectArgumentException.EmptyNameString();
        }

        Teacher = teacher;
        Time = time;
        Classroom = classroom;
        SubjectName = subjectName;
        Id = id;
    }

    public Teacher Teacher { get; }

    public LessonTime Time { get; }
    public Classroom Classroom { get; }
    public string SubjectName { get; }
    public Guid Id { get; }
}