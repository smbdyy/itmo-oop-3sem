using Isu.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Services;

public class IsuExtraService
{
    public IsuExtraService(TimeOnly lessonsStartTime, TimeOnly lessonsMaxEndTime, TimeSpan lessonDuration, TimeSpan breakDuration)
    {
        if (lessonsStartTime > lessonsMaxEndTime)
        {
            throw new NotImplementedException();
        }

        if (lessonsStartTime.Add(lessonDuration) > lessonsMaxEndTime)
        {
            throw new NotImplementedException();
        }

        LessonsStartTime = lessonsStartTime;
        LessonsMaxEndTime = lessonsMaxEndTime;
        LessonDuration = lessonDuration;
        BreakDuration = breakDuration;

        MaxLessonsAmount = Convert.ToInt32(Math.Truncate((LessonsMaxEndTime - LessonsStartTime) / (LessonDuration + BreakDuration)));
        if (LessonsStartTime.Add(((LessonDuration + BreakDuration) * MaxLessonsAmount) + LessonDuration) <= LessonsMaxEndTime)
        {
            MaxLessonsAmount++;
        }
    }

    public TimeOnly LessonsStartTime { get; }
    public TimeOnly LessonsMaxEndTime { get; }
    public TimeSpan LessonDuration { get; }
    public TimeSpan BreakDuration { get; }

    public int MaxLessonsAmount { get; }

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
            throw new NotImplementedException();
        }

        if (number < MaxLessonsAmount)
        {
            throw new NotImplementedException();
        }

        return number == 1 ? LessonsStartTime : LessonsStartTime.Add((LessonDuration + BreakDuration) * number);
    }
}