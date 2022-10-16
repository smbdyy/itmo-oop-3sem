using Isu.Services;

namespace Isu.Extra.Services;

public class IsuExtraServiceBuilder
{
    private TimeOnly _lessonsStartTime = new (8, 20);
    private TimeOnly _lessonsMaxEndTime = new (22, 0);
    private TimeSpan _lessonDuration = new (1, 30, 0);
    private TimeSpan _breakDuration = new (0, 10, 10);
    private IsuService _isuService = new IsuService();

    public IsuExtraServiceBuilder WithLessonsStartTime(TimeOnly lessonsStartTime)
    {
        _lessonsStartTime = lessonsStartTime;
        return this;
    }

    public IsuExtraServiceBuilder WithLessonsMaxEndTime(TimeOnly lessonsMaxEndTime)
    {
        _lessonsMaxEndTime = lessonsMaxEndTime;
        return this;
    }

    public IsuExtraServiceBuilder WithLessonDuration(TimeSpan lessonDuration)
    {
        _lessonDuration = lessonDuration;
        return this;
    }

    public IsuExtraServiceBuilder WithBreakDuration(TimeSpan breakDuration)
    {
        _breakDuration = breakDuration;
        return this;
    }

    public IsuExtraServiceBuilder WithIsuService(IsuService service)
    {
        _isuService = service;
        return this;
    }

    public IsuExtraService Build()
    {
        return new IsuExtraService(_lessonsStartTime, _lessonsMaxEndTime, _lessonDuration, _breakDuration, _isuService);
    }
}