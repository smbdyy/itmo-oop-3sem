using Isu.Models;

namespace Isu.Exceptions;

public class GroupIsNotFoundException : Exception
{
    public GroupIsNotFoundException() { }

    public GroupIsNotFoundException(GroupName groupName)
    {
        Message = $"group with name {groupName.AsString()} is not found";
    }

    public override string Message { get; } = "group is not found";
}