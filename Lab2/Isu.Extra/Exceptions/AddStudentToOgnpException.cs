using Isu.Entities;
using Isu.Extra.Entities;

namespace Isu.Extra.Exceptions;

public class AddStudentToOgnpException : Exception
{
    public AddStudentToOgnpException() { }

    public AddStudentToOgnpException(string message)
        : base(message) { }

    public static AddStudentToOgnpException SameMegafaculty(Student student, OgnpStream stream)
    {
        return new AddStudentToOgnpException(
            $"student {student.Name.AsString()}'s group has the same megafaculty as ognp course {stream.Course.Name}");
    }

    public static AddStudentToOgnpException SameCourse(Student student, OgnpStream stream)
    {
        return new AddStudentToOgnpException(
            $"student {student.Name.AsString()} is already a member of ognp course {stream.Course.Name}");
    }

    public static AddStudentToOgnpException AlreadyHasTwoCourses(Student student)
    {
        return new AddStudentToOgnpException(
            $"student {student.Name.AsString()} is already a member of two ognp courses");
    }

    public static AddStudentToOgnpException MaxMembersExceeded(Student student, OgnpStream stream)
    {
        return new AddStudentToOgnpException(
            $"cannot add student {student.Name.AsString()} to ognp stream {stream.Name}, it already has max members amount");
    }

    public static AddStudentToOgnpException ScheduleIntersect(Student student, OgnpStream stream)
    {
        return new AddStudentToOgnpException(
            $"ognp stream {stream.Name}'s schedule intersects with student {student.Name.AsString()}'s schedule");
    }
}