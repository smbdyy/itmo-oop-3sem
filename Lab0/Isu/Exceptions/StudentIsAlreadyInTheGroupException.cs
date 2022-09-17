using Isu.Entities;

namespace Isu.Exceptions;

public class StudentIsAlreadyInTheGroupException : Exception
{
    public StudentIsAlreadyInTheGroupException(Student student)
    {
        this.Message =
            $"student {student.Name.AsString()} is already in this group {student.Group.Name.AsString()}";
    }

    public override string Message { get; }
}