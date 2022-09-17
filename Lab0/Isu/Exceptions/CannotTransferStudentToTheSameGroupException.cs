using Isu.Entities;

namespace Isu.Exceptions;

public class CannotTransferStudentToTheSameGroupException : Exception
{
    public CannotTransferStudentToTheSameGroupException() { }

    public CannotTransferStudentToTheSameGroupException(Student student)
    {
        Message = $"cannot transfer student {student.Name} to their own group {student.Group.Name.AsString()}";
    }

    public override string Message { get; } = "cannot transfer student to their own group";
}