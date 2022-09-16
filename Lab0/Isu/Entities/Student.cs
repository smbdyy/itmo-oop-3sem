using System.Text.RegularExpressions;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(Group group, StudentName name)
    {
        Name = name;
        Group = group;
    }

    public int Id { get; }
    public StudentName Name { get; }
    public Group Group { get; }
}