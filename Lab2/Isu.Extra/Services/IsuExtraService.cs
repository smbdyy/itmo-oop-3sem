using Isu.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Services;

public class IsuExtraService
{
    public TimeOnly LessonsStartTime { get; } = new TimeOnly(8, 20);
    public TimeOnly LessonsMaxEndTime { get; } = new TimeOnly(22, 0);
    public TimeSpan LessonDuration { get; } = new TimeSpan(1, 30, 0);
    public TimeSpan BreakDuration { get; } = new TimeSpan(0, 10, 10);

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

    public int CountMaxLessonsAmount()
    {
        int count = Convert.ToInt32(Math.Truncate((LessonsMaxEndTime - LessonsStartTime) / (LessonDuration + BreakDuration)));
        if (LessonsStartTime.Add(((LessonDuration + BreakDuration) * count) + LessonDuration) <= LessonsMaxEndTime)
        {
            count++;
        }

        return count;
    }

    public TimeOnly GetLessonStartTime(int number)
    {
        if (number < 1)
        {
            // TODO
            throw new NotImplementedException();
        }

        if (number < CountMaxLessonsAmount())
        {
            // TODO
            throw new NotImplementedException();
        }

        return number == 1 ? LessonsStartTime : LessonsStartTime.Add((LessonDuration + BreakDuration) * number);
    }
}