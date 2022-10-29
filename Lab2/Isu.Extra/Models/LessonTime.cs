using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class LessonTime
{
    public LessonTime(int timeId, DayOfWeek week)
    {
        if (timeId < 1)
        {
            throw IncorrectArgumentException.TimeIdIsLessThanOne(timeId);
        }

        TimeId = timeId;
        Week = week;
    }

    public int TimeId { get; }
    public DayOfWeek Week { get; }

    public static bool operator ==(LessonTime a, LessonTime b)
    {
        return a.TimeId == b.TimeId && a.Week == b.Week;
    }

    public static bool operator !=(LessonTime a, LessonTime b)
    {
        return !(a == b);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        return obj.GetType() == GetType() && this == (LessonTime)obj;
    }

    public override int GetHashCode()
    {
        return (int)((TimeId * 10) + Week);
    }
}