using Isu.Entities;

namespace Isu.Exceptions;

public class StudentIsInAnotherGroupException : Exception
{
    public StudentIsInAnotherGroupException(Student student)
    {
        this.Message =
            $"student {student.GetNameAsString()} already in another group {student.Group.Name.GetNameAsString()}";
    }

    public override string Message { get; } = string.Empty;
}