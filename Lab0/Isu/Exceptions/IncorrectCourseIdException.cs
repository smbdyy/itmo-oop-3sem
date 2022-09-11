namespace Isu.Exceptions;

public class IncorrectCourseIdException : Exception
{
    public IncorrectCourseIdException() { }

    public IncorrectCourseIdException(char courseId)
    {
        if (courseId is > 'a' and < 'z')
        {
            this.Message = $"course id must be a capital letter ({courseId} is given)";
        }
    }

    public override string Message { get; } = "course id must be a capital latin letter";
}