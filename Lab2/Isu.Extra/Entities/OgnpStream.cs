using Isu.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class OgnpStream
{
    public OgnpStream(OgnpCourse course, int maxMembers)
    {
        Course = course;
        Id = Guid.NewGuid();
        if (maxMembers < 1)
        {
            throw new NotImplementedException();
        }

        MaxMembers = maxMembers;
    }

    public OgnpCourse Course { get; }
    public Guid Id { get; }
    public int MaxMembers { get; }
}