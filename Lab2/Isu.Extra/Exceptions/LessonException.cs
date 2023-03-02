using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Exceptions;

public class LessonException : Exception
{
    public LessonException(string? message)
        : base(message) { }

    public static LessonException SameStreamTimeIntersect(OgnpStream stream, LessonTime time)
    {
        return new LessonException(
            $"stream {stream.Name} has another lesson number {time.TimeId} at {time.Week.ToString()}");
    }

    public static LessonException SameGroupTimeIntersect(Group group, LessonTime time)
    {
        return new LessonException(
            $"stream {group.Name.AsString()} has another lesson number {time.TimeId} at {time.Week.ToString()}");
    }

    public static LessonException ClassroomIntersect(LessonTime time, Classroom classroom)
    {
        return new LessonException(
            $"another lesson number {time.TimeId} takes place in classroom {classroom.Name} at {time.Week.ToString()}");
    }
}