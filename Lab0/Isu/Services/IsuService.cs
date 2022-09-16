using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private List<Group> _groups = new ();
    private List<Student> _students = new ();

    public Group AddGroup(GroupName name, int maxStudentsAmount = Group.DefaultMaxStudentsAmount)
    {
        if (_groups.Any(group => group.Name == name))
        {
            throw new GroupWithGivenNameAlreadyExistsException(name);
        }

        var newGroup = new Group(name, maxStudentsAmount);
        _groups.Add(newGroup);
        return newGroup;
    }

    public Student AddStudent(Group group, StudentName name)
    {
        if (_students.Count(student => student.Group == group) == group.MaxStudentsAmount)
        {
            throw new MaxStudentsAmountExceededException(group);
        }

        int id = _students.Any() ? 100000 : _students.Last().Id + 1;
        var newStudent = new Student(group, name, id);
        _students.Add(newStudent);
        return newStudent;
    }
}
