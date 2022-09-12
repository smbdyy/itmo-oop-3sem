using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private int _maxStudentsAmount = 30;
    public Group(GroupName name)
    {
        this.Name = name;
    }

    public Group(GroupName name, List<Student> students)
        : this(name)
    {
        if (students.Count > MaxStudentsAmount)
        {
            throw new MaxStudentsAmountExceededException(this, students.Count);
        }

        Students = students;
    }

    public Group(GroupName name, int maxStudentsAmount)
        : this(name)
    {
        if (maxStudentsAmount <= 0)
        {
            throw new IncorrectMaxStudentsAmountException(maxStudentsAmount);
        }

        _maxStudentsAmount = maxStudentsAmount;
    }

    public Group(GroupName name, int maxStudentsAmount, List<Student> students)
        : this(name, maxStudentsAmount)
    {
        if (students.Count > MaxStudentsAmount)
        {
            throw new MaxStudentsAmountExceededException(this, students.Count);
        }

        Students = students;
    }

    public GroupName Name { get; }

    public int MaxStudentsAmount
    {
        get
        {
            return _maxStudentsAmount;
        }
        set
        {
            if (value <= 0)
            {
                throw new IncorrectMaxStudentsAmountException(value);
            }

            _maxStudentsAmount = value;
        }
    }

    public List<Student> Students { get; } = new List<Student>();
}