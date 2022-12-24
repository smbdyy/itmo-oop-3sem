namespace Banks.Console.UserInteractionInterfaces;

public interface IUserInteractionInterface
{
    public int Read();
    public string? ReadLine();
    public void Write(string value);
    public void WriteLine(string value);
}