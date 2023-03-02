using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string? message)
        : base(message) { }

    public static NotFoundException TeacherById(Guid id)
    {
        return new NotFoundException($"teacher with id {id} is not found");
    }

    public static NotFoundException OgnpCourseById(Guid id)
    {
        return new NotFoundException($"ognp course with id {id} is not found");
    }

    public static NotFoundException CourseIsNotRegistered(OgnpCourse course)
    {
        return new NotFoundException($"course {course.Name} is not registered");
    }

    public static NotFoundException StreamIsNotRegistered(OgnpStream stream)
    {
        return new NotFoundException($"stream {stream.Name} is not registered");
    }

    public static NotFoundException GroupIsNotRegistered(Group group)
    {
        return new NotFoundException($"group {group.Name.AsString()} is not registered");
    }

    public static NotFoundException TeacherIsNotRegistered(Teacher teacher)
    {
        return new NotFoundException($"teacher {teacher.Name.AsString()} is not registered");
    }

    public static NotFoundException StudentIsNotThisStreamMember(Student student, OgnpStream stream)
    {
        return new NotFoundException($"student {student.Name.AsString()} is not a member of ognp stream {stream.Name}");
    }

    public static NotFoundException OgnpStreamMember(Student student)
    {
        return new NotFoundException($"student {student.Name.AsString()} is not in any ognp stream");
    }
}