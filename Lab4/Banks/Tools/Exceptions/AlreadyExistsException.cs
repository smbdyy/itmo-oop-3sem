using Banks.Banks;
using Banks.Clients;

namespace Banks.Tools.Exceptions;

public class AlreadyExistsException : Exception
{
    public AlreadyExistsException(string message)
        : base(message) { }

    public static AlreadyExistsException PairForPercent(decimal percent)
    {
        return new AlreadyExistsException($"start amount for percent {percent} already exists");
    }

    public static AlreadyExistsException PairForStartAmount(decimal amount)
    {
        return new AlreadyExistsException($"percent for start amount {amount} already exists");
    }

    public static AlreadyExistsException ClientSubscribed(BankClient client, IBank bank)
    {
        return new AlreadyExistsException($"client {client.Name.AsString} is already subscribed to bank {bank.Name}");
    }

    public static AlreadyExistsException ClientRegistered(BankClient client)
    {
        return new AlreadyExistsException($"client {client.Name.AsString} is already registered");
    }
}