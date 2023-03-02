using Isu.Models;

namespace Isu.Extra.Entities;

public class Teacher
{
    public Teacher(Guid id, PersonName name)
    {
        Name = name;
        Id = id;
    }

    public PersonName Name { get; }
    public Guid Id { get; }
}