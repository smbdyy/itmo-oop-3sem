namespace Isu.Extra.Models;

public class LessonTime
{
    public LessonTime(int timeId, DayOfWeek week)
    {
        if (timeId < 1)
        {
            throw new NotImplementedException();
        }

        TimeId = timeId;
        Week = week;
    }

    public int TimeId { get; }
    public DayOfWeek Week { get; }
}