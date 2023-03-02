using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public interface IIsuService
{
    Group AddGroup(GroupName name, int maxStudentsAmount);
    Student AddStudent(Group group, PersonName name);

    Student GetStudent(int id);
    Student? FindStudent(int id);
    IReadOnlyCollection<Student> FindStudents(GroupName groupName);
    IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber);

    Group? FindGroup(GroupName groupName);
    List<Group> FindGroups(CourseNumber courseNumber);

    void ChangeStudentGroup(Student student, Group newGroup);
}