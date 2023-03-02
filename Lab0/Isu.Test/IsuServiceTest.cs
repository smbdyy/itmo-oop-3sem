using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var service = new IsuService();
        var groupName = new GroupName('M', new CourseNumber(AcademicDegree.Bachelor, 1), 3);
        Group group = service.AddGroup(groupName);
        Student student = service.AddStudent(group, new PersonName("Ilya", "Videneev"));
        Assert.Equal(group, student.Group);
        Assert.Contains(student, service.FindStudents(groupName));
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var service = new IsuService();
        var groupName = new GroupName('M', new CourseNumber(AcademicDegree.Bachelor, 1), 3);
        Group group = service.AddGroup(groupName, 1);
        service.AddStudent(group, new PersonName("Ilya", "Videneev"));
        Assert.Throws<GroupException>(() => service.AddStudent(group, new PersonName("Ivan", "Ivanov")));
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.Throws<IncorrectArgumentException>(() => new CourseNumber(AcademicDegree.Bachelor, 5));

        var validCourseNumber = new CourseNumber(AcademicDegree.Bachelor, 2);
        Assert.Throws<IncorrectArgumentException>(() => new GroupName('!', validCourseNumber, 0));
        Assert.Throws<IncorrectArgumentException>(() => new GroupName('M', validCourseNumber, 1000));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var service = new IsuService();
        var groupName = new GroupName('M', new CourseNumber(AcademicDegree.Bachelor, 1), 3);
        Group group = service.AddGroup(groupName);
        Student student = service.AddStudent(group, new PersonName("Ilya", "Videneev"));

        var newGroupName = new GroupName('M', new CourseNumber(AcademicDegree.Bachelor, 1), 4);
        Group newGroup = service.AddGroup(newGroupName);
        service.ChangeStudentGroup(student, newGroup);

        Assert.Equal(newGroup, student.Group);
    }
}