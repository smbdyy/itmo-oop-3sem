using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraService
{
    private readonly List<OgnpCourse> _ognpCourses = new ();
    private readonly List<OgnpStream> _ognpStreams = new ();
    private readonly List<GroupLesson> _groupLessons = new ();
    private readonly List<OgnpLesson> _ognpLessons = new ();
    private readonly List<OgnpStreamMember> _ognpStreamMembers = new ();
    private readonly List<Teacher> _teachers = new ();

    public IsuExtraService(
        TimeOnly lessonsStartTime,
        TimeOnly lessonsMaxEndTime,
        TimeSpan lessonDuration,
        TimeSpan breakDuration,
        IsuService isuService)
    {
        if (lessonsStartTime > lessonsMaxEndTime)
        {
            throw ServiceBuildException.LessonsEndTimeBeforeStartTime(lessonsStartTime, lessonsMaxEndTime);
        }

        if (lessonsStartTime.Add(lessonDuration) > lessonsMaxEndTime)
        {
            throw ServiceBuildException.CannotAddASingleLesson(lessonsStartTime, lessonsMaxEndTime, lessonDuration);
        }

        if (lessonDuration == TimeSpan.Zero || breakDuration == TimeSpan.Zero)
        {
            throw ServiceBuildException.DurationIsZero();
        }

        LessonsStartTime = lessonsStartTime;
        LessonsMaxEndTime = lessonsMaxEndTime;
        LessonDuration = lessonDuration;
        BreakDuration = breakDuration;
        IsuService = isuService;

        MaxLessonsAmount =
            Convert.ToInt32(
                Math.Truncate((LessonsMaxEndTime - LessonsStartTime) / (LessonDuration + BreakDuration)));

        if (LessonsStartTime
                .Add(((LessonDuration + BreakDuration) * MaxLessonsAmount) + LessonDuration) <= LessonsMaxEndTime)
        {
            MaxLessonsAmount++;
        }
    }

    public TimeOnly LessonsStartTime { get; }
    public TimeOnly LessonsMaxEndTime { get; }
    public TimeSpan LessonDuration { get; }
    public TimeSpan BreakDuration { get; }

    public int MaxLessonsAmount { get; }

    public IsuService IsuService { get; }

    public static Megafaculty GetMegafaculty(GroupName group)
    {
        return group.SpecialtyId switch
        {
            // megafaculty is identified by the first letter (SpecialtyId) of group name
            // A-F -- CTM
            // G-L -- Photonics
            // M-R -- TInT
            // S-Z -- BioTech
            >= 'A' and <= 'F' => Megafaculty.CTM,
            >= 'G' and <= 'L' => Megafaculty.Photonics,
            >= 'M' and <= 'R' => Megafaculty.TInT,
            _ => Megafaculty.BioTech
        };
    }

    public TimeOnly GetLessonStartTime(int number)
    {
        if (number < 1)
        {
            throw IncorrectArgumentException.TimeIdIsLessThanOne(number);
        }

        if (number > MaxLessonsAmount)
        {
            throw IncorrectArgumentException.TimeIdIsMoreThanMax(number, MaxLessonsAmount);
        }

        return number == 1 ? LessonsStartTime : LessonsStartTime.Add((LessonDuration + BreakDuration) * number);
    }

    public Teacher AddTeacher(PersonName name)
    {
        var teacher = new Teacher(Guid.NewGuid(), name);
        _teachers.Add(teacher);
        return teacher;
    }

    public Teacher GetTeacher(Guid id)
    {
        Teacher? teacher = _teachers.FirstOrDefault(teacher => teacher.Id == id);
        if (teacher is null)
        {
            throw NotFoundException.TeacherById(id);
        }

        return teacher;
    }

    public OgnpCourse GetOgnpCourse(Guid id)
    {
        OgnpCourse? course = _ognpCourses.FirstOrDefault(course => course.Id == id);
        if (course is null)
        {
            throw NotFoundException.OgnpCourseById(id);
        }

        return course;
    }

    public OgnpStreamMember? FindOgnpStreamMember(Student student)
    {
        return _ognpStreamMembers.FirstOrDefault(member => member.Student == student);
    }

    public OgnpStreamMember GetOgnpStreamMember(Student student)
    {
        OgnpStreamMember? member = FindOgnpStreamMember(student);

        if (member is null)
        {
            throw NotFoundException.OgnpStreamMember(student);
        }

        return member;
    }

    public OgnpCourse CreateOgnpCourse(string name, Megafaculty megafaculty)
    {
        var course = new OgnpCourse(Guid.NewGuid(), name, megafaculty);
        _ognpCourses.Add(course);
        return course;
    }

    public OgnpStream AddOgnpStream(OgnpCourse course, int maxMembers, string name)
    {
        if (!_ognpCourses.Contains(course))
        {
            throw NotFoundException.CourseIsNotRegistered(course);
        }

        var stream = new OgnpStream(Guid.NewGuid(), course, maxMembers, name);
        _ognpStreams.Add(stream);
        return stream;
    }

    public IEnumerable<OgnpLesson> GetLessons(OgnpStream stream)
    {
        if (!_ognpStreams.Contains(stream))
        {
            throw NotFoundException.StreamIsNotRegistered(stream);
        }

        return new List<OgnpLesson>(_ognpLessons.Where(lesson => lesson.Stream == stream));
    }

    public IEnumerable<GroupLesson> GetLessons(Group group)
    {
        if (IsuService.FindGroup(group.Name) is null)
        {
            throw NotFoundException.GroupIsNotRegistered(group);
        }

        return new List<GroupLesson>(_groupLessons.Where(lesson => lesson.Group == group));
    }

    public IEnumerable<OgnpStream> GetOgnpStreams(OgnpCourse course)
    {
        return _ognpStreams.Where(stream => stream.Course == course).ToList();
    }

    public IEnumerable<Student> GetOgnpStreamMembers(OgnpStream stream)
    {
        return _ognpStreamMembers.Where(member => member.Stream == stream).Select(member => member.Student).ToList();
    }

    public IEnumerable<Student> GetStudentsWithNoOgnpSelected(Group group)
    {
        return IsuService.FindStudents(group.Name)
            .Where(student => _ognpStreamMembers.All(member => member.Student != student));
    }

    public OgnpLesson AddOgnpLesson(OgnpStream stream, Teacher teacher, LessonTime time, Classroom classroom, string subjectName)
    {
        if (time.TimeId > MaxLessonsAmount)
        {
            throw IncorrectArgumentException.TimeIdIsMoreThanMax(time.TimeId, MaxLessonsAmount);
        }

        if (!_teachers.Contains(teacher))
        {
            throw NotFoundException.TeacherIsNotRegistered(teacher);
        }

        if (GetLessons(stream).Any(lesson => lesson.Time == time))
        {
            throw LessonException.SameStreamTimeIntersect(stream, time);
        }

        if (_groupLessons.Any(lesson => lesson.Classroom.Name == classroom.Name && lesson.Time == time))
        {
            throw LessonException.ClassroomIntersect(time, classroom);
        }

        if (_ognpLessons.Any(lesson => lesson.Classroom.Name == classroom.Name && lesson.Time == time))
        {
            throw LessonException.ClassroomIntersect(time, classroom);
        }

        var lesson = new OgnpLesson(Guid.NewGuid(), teacher, time, classroom, subjectName, stream);
        _ognpLessons.Add(lesson);
        return lesson;
    }

    public GroupLesson AddLesson(Group group, Teacher teacher, LessonTime time, Classroom classroom, string subjectName)
    {
        if (time.TimeId > MaxLessonsAmount)
        {
            throw IncorrectArgumentException.TimeIdIsMoreThanMax(time.TimeId, MaxLessonsAmount);
        }

        if (!_teachers.Contains(teacher))
        {
            throw NotFoundException.TeacherIsNotRegistered(teacher);
        }

        if (GetLessons(group).Any(lesson => lesson.Time == time))
        {
            throw LessonException.SameGroupTimeIntersect(group, time);
        }

        if (_ognpLessons.Any(lesson => lesson.Classroom.Name == classroom.Name && lesson.Time == time))
        {
            throw LessonException.ClassroomIntersect(time, classroom);
        }

        var lesson = new GroupLesson(Guid.NewGuid(), teacher, time, classroom, subjectName, group);
        _groupLessons.Add(lesson);
        return lesson;
    }

    public OgnpStreamMember AddStudentToOgnpStream(Student student, OgnpStream stream)
    {
        // check if student registered, throw exception if they're not
        IsuService.GetStudent(student.Id);

        if (!_ognpStreams.Contains(stream))
        {
            throw NotFoundException.StreamIsNotRegistered(stream);
        }

        if (stream.MaxMembers == _ognpStreamMembers.Count(member => member.Stream == stream))
        {
            throw OgnpException.MaxMembersExceeded(student, stream);
        }

        if (stream.Course.Megafaculty == GetMegafaculty(student.Group.Name))
        {
            throw OgnpException.SameMegafaculty(student, stream);
        }

        if (_ognpStreamMembers.Count(member => member.Student == student) == 2)
        {
            throw OgnpException.AlreadyHasTwoCourses(student);
        }

        if (GetLessons(student.Group).Any(lesson => GetLessons(stream).Any(l => l.Time == lesson.Time)))
        {
            throw OgnpException.ScheduleIntersect(student, stream);
        }

        OgnpStreamMember? studentAsStreamMember =
            _ognpStreamMembers.FirstOrDefault(member => member.Student == student);

        if (studentAsStreamMember is not null)
        {
            OgnpStream studentStream = studentAsStreamMember.Stream;

            if (studentStream.Course == stream.Course)
            {
                throw OgnpException.SameCourse(student, stream);
            }

            if (GetLessons(studentStream).Any(lesson => GetLessons(stream).Any(l => l.Time == lesson.Time)))
            {
                throw OgnpException.ScheduleIntersect(student, stream);
            }
        }

        var member = new OgnpStreamMember(student, stream);
        _ognpStreamMembers.Add(member);
        return member;
    }

    public void RemoveOgnpStreamMember(Student student, OgnpStream stream)
    {
        IsuService.GetStudent(student.Id);

        if (!_ognpStreams.Contains(stream))
        {
            throw NotFoundException.StreamIsNotRegistered(stream);
        }

        OgnpStreamMember? studentAsStreamMember =
            _ognpStreamMembers.FirstOrDefault(member => member.Student == student && member.Stream == stream);

        if (studentAsStreamMember is null)
        {
            throw NotFoundException.StudentIsNotThisStreamMember(student, stream);
        }

        _ognpStreamMembers.Remove(studentAsStreamMember);
    }
}