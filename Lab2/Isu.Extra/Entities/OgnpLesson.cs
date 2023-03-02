using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class OgnpLesson : Lesson
{
    public OgnpLesson(
        Guid id, Teacher teacher, LessonTime time, Classroom classroom, string subjectName, OgnpStream stream)
        : base(id, teacher, time, classroom, subjectName)
    {
        Stream = stream;
    }

    public OgnpStream Stream { get; }
}