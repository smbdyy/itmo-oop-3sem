using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public struct Classroom
{
    public Classroom(int block, int floor, int number)
    {
        if (block is < 1 or > 9)
        {
            throw IncorrectArgumentException.ClassroomBlock(block);
        }

        if (floor is < 1 or > 9)
        {
            throw IncorrectArgumentException.ClassroomFloor(floor);
        }

        if (number is < 1 or > 99)
        {
            throw IncorrectArgumentException.ClassroomNumber(number);
        }

        Name = (block * 1000) + (floor * 100) + number;
    }

    public int Name { get; }
}