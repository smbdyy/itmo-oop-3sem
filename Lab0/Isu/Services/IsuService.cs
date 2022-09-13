using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private List<Group> _groups = new ();
    private List<Student> _students = new ();

    public Group AddGroup(GroupName name)
    {
        bool groupWithGivenNameExists = _groups.Any(group => group.Name == name);
        if (groupWithGivenNameExists)
        {
            throw new GroupWithGivenNameAlreadyExistsException(name);
        }

        var newGroup = new Group(name);
        _groups.Add(newGroup);
        return newGroup;
    }

}
