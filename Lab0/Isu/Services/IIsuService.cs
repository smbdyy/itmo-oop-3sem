using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public interface IIsuService
{
    Group AddGroup(GroupName name, int maxStudentsAmount = Group.DefaultMaxStudentsAmount);
    Student AddStudent(Group group, PersonName name);

    Student GetStudent(int id);
    Student? FindStudent(int id);
    List<Student> FindStudents(GroupName groupName);
    List<Student> FindStudents(CourseNumber courseNumber);

    Group? FindGroup(GroupName groupName);
    List<Group> FindGroups(CourseNumber courseNumber);

    void ChangeStudentGroup(Student student, Group newGroup);
}