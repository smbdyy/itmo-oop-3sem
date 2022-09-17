using Isu.Entities;

namespace Isu.Exceptions;

public class StudentIsInAnotherGroupException : Exception
{
    public StudentIsInAnotherGroupException(Student student)
    {
        this.Message =
            $"student {student.Name.AsString()} already in another group {student.Group.Name.AsString()}";
    }

    public override string Message { get; } = string.Empty;
}