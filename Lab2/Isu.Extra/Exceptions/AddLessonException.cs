using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Exceptions;

public class AddLessonException : Exception
{
    public AddLessonException() { }

    public AddLessonException(string message)
        : base(message) { }

    public static AddLessonException SameStreamTimeIntersect(OgnpStream stream, LessonTime time)
    {
        return new AddLessonException(
            $"stream {stream.Name} has another lesson number {time.TimeId} at {time.Week.ToString()}");
    }

    public static AddLessonException SameGroupTimeIntersect(Group group, LessonTime time)
    {
        return new AddLessonException(
            $"stream {group.Name.AsString()} has another lesson number {time.TimeId} at {time.Week.ToString()}");
    }

    public static AddLessonException ClassroomIntersect(LessonTime time, Classroom classroom)
    {
        return new AddLessonException(
            $"another lesson number {time.TimeId} takes place in classroom {classroom.Name} at {time.Week.ToString()}");
    }
}