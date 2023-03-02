using Isu.Entities;
using Isu.Models;

namespace Isu.Exceptions;

public class GroupException : IsuException
{
    public GroupException(string? message)
        : base(message) { }

    public static GroupException NameTaken(GroupName name)
    {
        return new GroupException($"group with name {name} already exists");
    }

    public static GroupException StudentsLimitExceeded(Group group)
    {
        return new GroupException(
            $"group {group.Name.AsString()} has already reached max students amount {group.MaxStudentsAmount}");
    }
}