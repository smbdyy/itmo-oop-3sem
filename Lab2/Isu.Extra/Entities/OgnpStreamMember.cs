using Isu.Entities;

namespace Isu.Extra.Entities;

public class OgnpStreamMember
{
    public OgnpStreamMember(Student student, OgnpStream stream)
    {
        Student = student;
        Stream = stream;
    }

    public Student Student { get; }
    public OgnpStream Stream { get; }
}