namespace Isu.Models;

public class GroupName
{
    public GroupName(char courseId, CourseNumber courseNumber, int groupNumber)
    {
        if (courseId is < 'A' or > 'Z')
        {
            // throw
        }

        if (groupNumber is < 0 or > 99)
        {
            // throw
        }

        this.CourseId = courseId;
        this.CourseNum = courseNumber;
        this.GroupNumber = groupNumber;
    }

    private char CourseId { get; }
    private CourseNumber CourseNum { get; }
    private int GroupNumber { get; }
}