namespace Banks.Tools.Exceptions;

public class RequiredFieldInBuilderIsNullException : Exception
{
    public RequiredFieldInBuilderIsNullException()
        : base("cannot create object with null field in builder") { }
}