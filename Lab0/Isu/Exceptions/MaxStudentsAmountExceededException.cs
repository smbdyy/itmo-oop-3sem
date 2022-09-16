using Isu.Entities;

namespace Isu.Exceptions;

public class MaxStudentsAmountExceededException : Exception
{
    public MaxStudentsAmountExceededException() { }

    public MaxStudentsAmountExceededException(Group group)
    {
        Message = $"max students amount ({group.MaxStudentsAmount} exceeded in group {group.Name.AsString()}";
    }

    public MaxStudentsAmountExceededException(Group group, int studentsListSize)
    {
        Message =
            $"max students amount ({group.MaxStudentsAmount}) exceeded in group {group.Name.AsString()} ({studentsListSize} students given)";
    }

    public override string Message { get; } = "max students amount for group exceeded";
}