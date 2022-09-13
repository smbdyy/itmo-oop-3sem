using Isu.Models;

namespace Isu.Exceptions;

public class GroupWithGivenNameAlreadyExistsException : Exception
{
    public GroupWithGivenNameAlreadyExistsException() { }

    public GroupWithGivenNameAlreadyExistsException(GroupName name)
    {
        Message = "group with name {name} already exists";
    }

    public override string Message { get; } = "group with given name already exists";
}