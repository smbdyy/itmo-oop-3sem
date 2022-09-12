using Isu.Entities;

namespace Isu.Exceptions;

public class StudentIsAlreadyInTheGroupException : Exception
{
    public StudentIsAlreadyInTheGroupException(Student student)
    {
        this.Message =
            $"student {student.GetNameAsString()} is already in this group {student.Group.Name.GetNameAsString()}";
    }

    public override string Message { get; }
}