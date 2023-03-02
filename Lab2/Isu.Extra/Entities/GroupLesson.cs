using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class GroupLesson : Lesson
{
    public GroupLesson(
        Guid id, Teacher teacher, LessonTime time, Classroom classroom, string subjectNameGroup, Group group)
        : base(id, teacher, time, classroom, subjectNameGroup)
    {
        Group = group;
    }

    public Group Group { get; }
}