namespace Isu.Exceptions;

public class CannotGenerateNewStudentIdException : Exception
{
    public override string Message { get; } = "cannot generate new id, because last student's id is 999999";
}