using Isu.Entities;

namespace Isu.Exceptions;

public class IncorrectStudentNameException : Exception
{
    public IncorrectStudentNameException() { }

    public IncorrectStudentNameException(Student student)
    {
        Message =
            $"name, surname and patronymic must start with a capital letter, next letters must be from the same alphabet ({student.GetNameAsString()} is given)";
    }

    public override string Message { get; } =
        "name, surname and patronymic must start with a capital letter, next letters must be from the same alphabet";
}