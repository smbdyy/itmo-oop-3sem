namespace Isu.Exceptions;

public class StudentIsNotFoundException : Exception
{
    public StudentIsNotFoundException() { }

    public StudentIsNotFoundException(int id)
    {
        Message = "student with id {id} is not found";
    }

    public override string Message { get; } = "student with provided id is not found";
}