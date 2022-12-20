﻿namespace Banks.Models;

public class ConsoleNotificationReceiver : NotificationReceiver
{
    public override void Receive(string message)
    {
        Console.WriteLine(message);
        base.Receive(message);
    }
}