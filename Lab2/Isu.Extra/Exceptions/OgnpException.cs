using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class OgnpException : Exception
{
    public OgnpException() { }

    public OgnpException(string? message)
        : base(message) { }

    public static OgnpException SameMegafaculty(Student student, OgnpStream stream)
    {
        return new OgnpException(
            $"student {student.Name.AsString()}'s group has the same megafaculty as ognp course {stream.Course.Name}");
    }

    public static OgnpException SameCourse(Student student, OgnpStream stream)
    {
        return new OgnpException(
            $"student {student.Name.AsString()} is already a member of ognp course {stream.Course.Name}");
    }

    public static OgnpException AlreadyHasTwoCourses(Student student)
    {
        return new OgnpException(
            $"student {student.Name.AsString()} is already a member of two ognp courses");
    }

    public static OgnpException MaxMembersExceeded(Student student, OgnpStream stream)
    {
        return new OgnpException(
            $"cannot add student {student.Name.AsString()} to ognp stream {stream.Name}, it already has max members amount");
    }

    public static OgnpException ScheduleIntersect(Student student, OgnpStream stream)
    {
        return new OgnpException(
            $"ognp stream {stream.Name}'s schedule intersects with student {student.Name.AsString()}'s schedule");
    }
}