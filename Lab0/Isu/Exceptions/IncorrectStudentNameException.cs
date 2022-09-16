using Isu.Models;

namespace Isu.Exceptions;

public class IncorrectStudentNameException : Exception
{
    public IncorrectStudentNameException() { }

    public IncorrectStudentNameException(StudentName name)
    {
        Message =
            $"name, surname and patronymic must start with a capital letter, next letters must be from the same alphabet ({name.AsString()} is given)";
    }

    public override string Message { get; } =
        "name, surname and patronymic must start with a capital letter, next letters must be from the same alphabet";
}