using Isu.Models;

namespace Isu.Exceptions;

public class NotFoundException : IsuException
{
    public NotFoundException(string? message)
        : base(message) { }

    public static NotFoundException Group(GroupName groupName)
    {
        return new NotFoundException($"group with name {groupName.AsString()} is not found");
    }

    public static NotFoundException Student(int id)
    {
        return new NotFoundException($"student with id {id} is not found");
    }
}