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
}