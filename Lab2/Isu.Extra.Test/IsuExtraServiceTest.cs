using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Xunit;

namespace Isu.Extra.Test;

public class IsuExtraServiceTest
{
    [Fact]
    public void BuildIsuExtraServiceWithLessonsEndTimeEarlierThanStartTime_ThrowException()
    {
        IsuExtraServiceBuilder builder = new IsuExtraServiceBuilder()
            .SetLessonsStartTime(new TimeOnly(9, 0))
            .SetLessonsMaxEndTime(new TimeOnly(8, 0));

        Assert.Throws<ServiceBuildException>(() => builder.Build());
    }

    [Fact]
    public void BuildIsuExtraServiceImpossibleToAddALesson_ThrowException()
    {
        IsuExtraServiceBuilder builder = new IsuExtraServiceBuilder()
            .SetLessonsStartTime(new TimeOnly(8, 0))
            .SetLessonsMaxEndTime(new TimeOnly(9, 0))
            .SetLessonDuration(new TimeSpan(2, 0, 0));

        Assert.Throws<ServiceBuildException>(() => builder.Build());
    }

    [Fact]
    public void AddStreamToCourse_CourseContainsStream()
    {
        IsuExtraService service = new IsuExtraServiceBuilder().Build();
        OgnpCourse course = service.CreateOgnpCourse("new course", Megafaculty.Photonics);
        OgnpStream stream = service.AddOgnpStream(course, 3, "stream 1");

        Assert.Contains(stream, service.GetOgnpStreams(course));
    }

    [Fact]
    public void AddStudentToStream_StreamContainsStudent()
    {
        IsuExtraService service = new IsuExtraServiceBuilder().Build();
        OgnpCourse course = service.CreateOgnpCourse("new course", Megafaculty.Photonics);
        OgnpStream stream = service.AddOgnpStream(course, 3, "stream 1");
        Group group = service.IsuService.AddGroup(new GroupName('M', new CourseNumber(AcademicDegree.Bachelor, 1), 3));
        Student student = service.IsuService.AddStudent(group, new PersonName("Ivan", "Ivanov"));
        service.AddStudentToOgnpStream(student, stream);

        Assert.Contains(student, service.GetOgnpStreamMembers(stream));
    }

    [Fact]
    public void RemoveStudentFromStream_StreamDoesNotContainStudent()
    {
        IsuExtraService service = new IsuExtraServiceBuilder().Build();
        OgnpCourse course = service.CreateOgnpCourse("new course", Megafaculty.Photonics);
        OgnpStream stream = service.AddOgnpStream(course, 3, "stream 1");
        Group group = service.IsuService.AddGroup(new GroupName('M', new CourseNumber(AcademicDegree.Bachelor, 1), 3));
        Student student1 = service.IsuService.AddStudent(group, new PersonName("Ivan", "Ivanov"));
        Student student2 = service.IsuService.AddStudent(group, new PersonName("Ivan", "Petrov"));
        service.AddStudentToOgnpStream(student1, stream);
        service.AddStudentToOgnpStream(student2, stream);

        service.RemoveOgnpStreamMember(student1, stream);
        Assert.DoesNotContain(student1, service.GetOgnpStreamMembers(stream));
        service.RemoveOgnpStreamMember(student2, stream);
        Assert.DoesNotContain(student2, service.GetOgnpStreamMembers(stream));
    }

    [Fact]
    public void TryAssignStudentToThreeOgnpCourses_ThrowException()
    {
        IsuExtraService service = new IsuExtraServiceBuilder().Build();
        OgnpCourse course1 = service.CreateOgnpCourse("course 1", Megafaculty.Photonics);
        OgnpStream course1Stream = service.AddOgnpStream(course1, 3, "stream 1");
        OgnpCourse course2 = service.CreateOgnpCourse("course 2", Megafaculty.CTM);
        OgnpStream course2Stream = service.AddOgnpStream(course2, 3, "stream 1");
        OgnpCourse course3 = service.CreateOgnpCourse("course 3", Megafaculty.BioTech);
        OgnpStream course3Stream = service.AddOgnpStream(course3, 3, "stream 1");
        Group group = service.IsuService.AddGroup(new GroupName('M', new CourseNumber(AcademicDegree.Bachelor, 1), 3));
        Student student = service.IsuService.AddStudent(group, new PersonName("Ivan", "Ivanov"));
        service.AddStudentToOgnpStream(student, course1Stream);
        service.AddStudentToOgnpStream(student, course2Stream);

        Assert.Throws<OgnpException>(() => service.AddStudentToOgnpStream(student, course3Stream));
    }

    [Fact]
    public void LeaveFewStudentsWithoutAssigningToOgnp_GetStudentsWithNoOgnpSelectedCorrectly()
    {
        IsuExtraService service = new IsuExtraServiceBuilder().Build();
        OgnpCourse course = service.CreateOgnpCourse("new course", Megafaculty.Photonics);
        OgnpStream stream = service.AddOgnpStream(course, 3, "stream 1");
        Group group = service.IsuService.AddGroup(new GroupName('M', new CourseNumber(AcademicDegree.Bachelor, 1), 3));
        Student student1 = service.IsuService.AddStudent(group, new PersonName("Ivan", "Ivanov"));
        Student student2 = service.IsuService.AddStudent(group, new PersonName("Ivan", "Petrov"));
        Student student3 = service.IsuService.AddStudent(group, new PersonName("Petr", "Petrov"));
        Student student4 = service.IsuService.AddStudent(group, new PersonName("Petr", "Ivanov"));

        service.AddStudentToOgnpStream(student1, stream);
        service.AddStudentToOgnpStream(student2, stream);

        Assert.Equal(new List<Student> { student3, student4 }, service.GetStudentsWithNoOgnpSelected(group));
    }

    [Fact]
    public void AddLessonToGroupOrStream_ContainsLesson()
    {
        IsuExtraService service = new IsuExtraServiceBuilder().Build();
        OgnpCourse course = service.CreateOgnpCourse("new course", Megafaculty.Photonics);
        OgnpStream stream = service.AddOgnpStream(course, 3, "stream 1");
        Group group = service.IsuService.AddGroup(new GroupName('M', new CourseNumber(AcademicDegree.Bachelor, 1), 3));
        Teacher teacher = service.AddTeacher(new PersonName("Name", "Surname"));

        OgnpLesson ognpLesson = service
            .AddOgnpLesson(stream, teacher, new LessonTime(1, DayOfWeek.Monday), new Classroom(1, 1, 1), "Maths");
        Assert.Contains(ognpLesson, service.GetLessons(stream));

        GroupLesson groupLesson = service
            .AddLesson(group, teacher, new LessonTime(1, DayOfWeek.Tuesday), new Classroom(1, 1, 1), "Maths");
        Assert.Contains(groupLesson, service.GetLessons(group));
    }

    [Fact]
    public void AddIntersectingLesson_ThrowException()
    {
        IsuExtraService service = new IsuExtraServiceBuilder().Build();
        OgnpCourse course = service.CreateOgnpCourse("new course", Megafaculty.Photonics);
        OgnpStream stream1 = service.AddOgnpStream(course, 3, "stream 1");
        Group group = service.IsuService.AddGroup(new GroupName('M', new CourseNumber(AcademicDegree.Bachelor, 1), 3));
        Teacher teacher = service.AddTeacher(new PersonName("Name", "Surname"));
        service.AddOgnpLesson(stream1, teacher, new LessonTime(1, DayOfWeek.Monday), new Classroom(1, 1, 1), "Maths");

        Assert.Throws<LessonException>(() => service.AddOgnpLesson(
            stream1,
            teacher,
            new LessonTime(1, DayOfWeek.Monday),
            new Classroom(1, 1, 3),
            "Physics"));
        Assert.Throws<LessonException>(() => service.AddLesson(
            group,
            teacher,
            new LessonTime(1, DayOfWeek.Monday),
            new Classroom(1, 1, 1),
            "Programming"));

        OgnpStream stream2 = service.AddOgnpStream(course, 3, "stream 2");
        Assert.Throws<LessonException>(() => service.AddOgnpLesson(
            stream2,
            teacher,
            new LessonTime(1, DayOfWeek.Monday),
            new Classroom(1, 1, 1),
            "History"));
    }

    [Fact]
    public void StreamIntersectsWithStudentSchedule_ThrowException()
    {
        IsuExtraService service = new IsuExtraServiceBuilder().Build();
        OgnpCourse course = service.CreateOgnpCourse("new course", Megafaculty.Photonics);
        OgnpStream stream = service.AddOgnpStream(course, 3, "stream 1");
        Group group = service.IsuService.AddGroup(new GroupName('M', new CourseNumber(AcademicDegree.Bachelor, 1), 3));
        Student student = service.IsuService.AddStudent(group, new PersonName("Ivan", "Ivanov"));
        Teacher teacher = service.AddTeacher(new PersonName("Name", "Surname"));
        service.AddOgnpLesson(stream, teacher, new LessonTime(1, DayOfWeek.Monday), new Classroom(1, 1, 1), "Maths");
        service.AddLesson(group, teacher, new LessonTime(1, DayOfWeek.Monday), new Classroom(1, 1, 2), "Physics");

        Assert.Throws<OgnpException>(() => service.AddStudentToOgnpStream(student, stream));
    }
}