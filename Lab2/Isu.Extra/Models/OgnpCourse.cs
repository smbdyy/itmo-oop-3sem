using Isu.Extra.Exceptions;

namespace Isu.Extra.Models;

public class OgnpCourse
{
    public OgnpCourse(Guid id, string name, Megafaculty megafaculty)
    {
        if (name == string.Empty)
        {
            throw IncorrectArgumentException.EmptyNameString();
        }

        Name = name;
        Megafaculty = megafaculty;
        Id = id;
    }

    public Guid Id { get; }
    public string Name { get; }
    public Megafaculty Megafaculty { get; }
}