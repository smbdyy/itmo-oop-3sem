namespace Isu.Extra.Exceptions;

public class ServiceBuildException : Exception
{
    public ServiceBuildException() { }

    public ServiceBuildException(string? message)
        : base(message) { }

    public static ServiceBuildException LessonsEndTimeBeforeStartTime(TimeOnly start, TimeOnly end)
    {
        return new ServiceBuildException(
            $"lessons end time {end.ToString()} is earlier than start time {start.ToString()}");
    }

    public static ServiceBuildException DurationIsZero()
    {
        return new ServiceBuildException("lesson or break duration cannot be zero");
    }

    public static ServiceBuildException CannotAddASingleLesson(TimeOnly start, TimeOnly end, TimeSpan lessonDuration)
    {
        return new ServiceBuildException(
            $"impossible to add a single lesson with duration {lessonDuration.ToString()} with lessons start time {start.ToString()} and end time {end.ToString()}");
    }
}