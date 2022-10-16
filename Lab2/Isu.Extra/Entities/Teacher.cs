using Isu.Models;

namespace Isu.Extra.Entities;

public class Teacher
{
    public Teacher(PersonName name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }

    public PersonName Name { get; }
    public Guid Id { get; }
}