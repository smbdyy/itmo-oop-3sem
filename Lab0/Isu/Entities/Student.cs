using System.Text.RegularExpressions;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(Group group, PersonName name, int id)
    {
        Name = name;
        Group = group;
        Id = id;
    }

    public int Id { get; }
    public PersonName Name { get; }
    public Group Group { get; private set; }

    public void ChangeGroup(Group newGroup)
    {
        if (Group == newGroup)
        {
            throw new CannotTransferStudentToTheSameGroupException(this);
        }

        Group = newGroup;
    }
}