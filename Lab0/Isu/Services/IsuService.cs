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

        if (_students.Last().Id == 999999)
        {
            throw new CannotGenerateNewStudentIdException();
        }

        int id = _students.Any() ? 100000 : _students.Last().Id + 1;
        var newStudent = new Student(group, name, id);
        _students.Add(newStudent);
        return newStudent;
    }

    public Student GetStudent(int id)
    {
        Student? foundStudent = _students.Find(student => student.Id == id);
        if (foundStudent is null)
        {
            throw new StudentIsNotFoundException(id);
        }

        return foundStudent;
    }

    public Student? FindStudent(int id)
    {
        return _students.Find(student => student.Id == id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        Group? group = _groups.Find(group => group.Name == groupName);

        if (group is null)
        {
            throw new GroupIsNotFoundException(groupName);
        }

        return new List<Student>(_students.Where(student => student.Group == group));
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return new List<Student>(_students.Where(student => student.Group.Name.CourseNum == courseNumber));
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.Find(group => group.Name == groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return new List<Group>(_groups.Where(group => group.Name.CourseNum == courseNumber));
    }
}
