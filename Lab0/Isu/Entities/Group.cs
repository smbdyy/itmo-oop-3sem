using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    public Group(GroupName name)
    {
        Name = name;
    }

    public Group(GroupName name, int maxStudentsAmount)
        : this(name)
    {
        if (maxStudentsAmount <= 0)
        {
            throw new IncorrectMaxStudentsAmountException(maxStudentsAmount);
        }

        MaxStudentsAmount = maxStudentsAmount;
    }

    public GroupName Name { get; }

    public int MaxStudentsAmount { get; }
}