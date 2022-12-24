namespace Banks.Console.UserInteractionInterfaces;

public class ConsoleUserInteractionInterface : IUserInteractionInterface
{
    public int Read() => System.Console.Read();
    public string? ReadLine() => System.Console.ReadLine();
    public void Write(string value) => System.Console.Write(value);
    public void WriteLine(string value) => System.Console.WriteLine(value);
}