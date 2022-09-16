using Isu.Entities;
using Isu.Exceptions;

namespace Isu.Models;

public class GroupName
{
    // GroupName consists of specialty identifier (one capital latin letter),
    // course number (1-4 for bachelor or doctor degree, 1-2 for master degree)
    // and group number (0-99, two digits, eg. second group is 02)
    // therefore, btw, there can only be not more than 100 groups for each specialty and course number
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

    public string AsString()
    {
        return GroupNumber < 10 ? $"{SpecialtyId}{CourseNum.Number}0{GroupNumber}" : $"{SpecialtyId}{CourseNum.Number}{GroupNumber}";
    }
}