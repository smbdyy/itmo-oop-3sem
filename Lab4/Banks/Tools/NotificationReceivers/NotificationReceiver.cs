namespace Banks.Tools.NotificationReceivers;

public abstract class NotificationReceiver
{
    private NotificationReceiver? _next;

    public NotificationReceiver SetNext(NotificationReceiver next)
    {
        _next = next;
        return next;
    }

    public virtual void Receive(string message)
    {
        _next?.Receive(message);
    }
}