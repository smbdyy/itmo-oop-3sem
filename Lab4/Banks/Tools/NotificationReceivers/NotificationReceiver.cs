namespace Banks.Models;

public abstract class NotificationReceiver
{
    private NotificationReceiver? _next;

    public NotificationReceiver SetNext(NotificationReceiver next)
    {
        _next = next;
        return this;
    }

    public virtual void Receive(string message)
    {
        _next?.Receive(message);
    }
}