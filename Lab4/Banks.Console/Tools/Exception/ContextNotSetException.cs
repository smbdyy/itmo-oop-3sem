namespace Banks.Console.Tools.Exception;

public class ContextNotSetException : System.Exception
{
    public ContextNotSetException()
        : base("context not set") { }

    public ContextNotSetException(string message)
        : base(message) { }
}