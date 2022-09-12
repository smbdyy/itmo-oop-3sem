namespace Isu.Exceptions;

public class IncorrectSpecialtyIdException : Exception
{
    public IncorrectSpecialtyIdException() { }

    public IncorrectSpecialtyIdException(char specialtyId)
    {
        this.Message = $"specialty id must be a capital letter ({specialtyId} is given)";
    }

    public override string Message { get; } = "specialty id must be a capital latin letter";
}