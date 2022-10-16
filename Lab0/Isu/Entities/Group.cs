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
            throw new IncorrectMaxStudentsAmountException(maxStudentsAmount);
        }

        Name = name;
        MaxStudentsAmount = maxStudentsAmount;
        Id = Guid.NewGuid();
    }

    public GroupName Name { get; }
    public int MaxStudentsAmount { get; }
    public Guid Id { get; }
}