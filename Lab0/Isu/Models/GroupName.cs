using Isu.Exceptions;

namespace Isu.Models;

public class GroupName
{
    public GroupName(char specialtyId, CourseNumber courseNumber, int groupNumber)
    {
        if (specialtyId is < 'A' or > 'Z')
        {
            throw new IncorrectSpecialtyIdException(specialtyId);
        }

        if (groupNumber is < 0 or > 99)
        {
            throw new IncorrectGroupNumberException(groupNumber);
        }

        this.SpecialtyId = specialtyId;
        this.CourseNum = courseNumber;
        this.GroupNumber = groupNumber;
    }

    private char SpecialtyId { get; }
    private CourseNumber CourseNum { get; }
    private int GroupNumber { get; }
}