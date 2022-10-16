namespace Isu.Extra.Models;

public class OgnpCourse
{
    public OgnpCourse(string name, Megafaculty megafaculty)
    {
        if (name == string.Empty)
        {
            throw new NotImplementedException();
        }

        Name = name;
        Megafaculty = megafaculty;
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public string Name { get; }
    public Megafaculty Megafaculty { get; }
}