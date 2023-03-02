using Isu.Entities;

namespace Isu.Exceptions;

public class StudentException : IsuException
{
    public StudentException(string? message)
        : base(message) { }

    public static StudentException CannotCreateId()
    {
        return new StudentException("cannot generate new student id, last student id is 999999");
    }

    public static StudentException AlreadyInGroup(Student student)
    {
        return new StudentException(
            $"student {student.Name} is already in group {student.Group.Name.AsString()}");
    }
}