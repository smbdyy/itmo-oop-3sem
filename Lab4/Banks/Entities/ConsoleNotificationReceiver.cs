using Banks.Interfaces;

namespace Banks.Entities;

public class ConsoleNotificationReceiver : INotificationReceiver
{
    public void Receive(string message)
    {
        Console.WriteLine(message);
    }
}