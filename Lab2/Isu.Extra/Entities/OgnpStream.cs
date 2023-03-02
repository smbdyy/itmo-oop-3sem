using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class OgnpStream
{
    public OgnpStream(Guid id, OgnpCourse course, int maxMembers, string name)
    {
        Course = course;
        Id = id;
        if (maxMembers < 1)
        {
            throw IncorrectArgumentException.MaxMembersLessThanOne(maxMembers);
        }

        if (name == string.Empty)
        {
            throw IncorrectArgumentException.EmptyNameString();
        }

        MaxMembers = maxMembers;
        Name = name;
    }

    public OgnpCourse Course { get; }
    public Guid Id { get; }
    public int MaxMembers { get; }
    public string Name { get; }
}