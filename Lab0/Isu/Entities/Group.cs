using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    public const int DefaultMaxStudentsAmount = 30;

    public Group(GroupName name, int maxStudentsAmount)
    {
        if (maxStudentsAmount <= 0)
        {
            throw IncorrectArgumentException.MaxStudentsAmount(maxStudentsAmount);
        }

        Name = name;
        MaxStudentsAmount = maxStudentsAmount;
    }

    public Group(GroupName name)
    {
        Name = name;
        MaxStudentsAmount = DefaultMaxStudentsAmount;
    }

    public GroupName Name { get; }

    public int MaxStudentsAmount { get; }
}