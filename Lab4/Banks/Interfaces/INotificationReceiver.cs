namespace Banks.Interfaces;

public interface INotificationReceiver
{
    public void Receive(string message);
}