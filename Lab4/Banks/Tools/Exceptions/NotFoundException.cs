using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;

namespace Banks.Tools.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message) { }

    public static NotFoundException BankAccount(IBankAccount account)
    {
        return new NotFoundException(
            $"client {account.Client.Name.AsString}'s account is not found");
    }

    public static NotFoundException BankClient(BankClient client)
    {
        return new NotFoundException($"client {client.Name.AsString} is not found");
    }

    public static NotFoundException BankClientById(Guid clientId)
    {
        return new NotFoundException($"client {clientId} is not found");
    }

    public static NotFoundException BankClientInBank(BankClient client, IBank bank)
    {
        return new NotFoundException($"client {client.Name.AsString} is not found in bank {bank.Name}");
    }

    public static NotFoundException Transaction(Guid id, IBankAccount account)
    {
        return new NotFoundException(
            $"transaction {id} is not found in " +
            $"{account.Client.Name.AsString}'s account");
    }

    public static NotFoundException Bank(IBank bank)
    {
        return new NotFoundException($"bank {bank.Name} is not found");
    }

    public static NotFoundException BankById(Guid id)
    {
        return new NotFoundException($"bank with id {id} is not found");
    }

    public static NotFoundException StartAmountPercentPair(StartAmountPercentPair pair, IBank bank)
    {
        return new NotFoundException(
            $"start amount -- percent pair {pair.StartAmount} -- " +
            $"{pair.Percent} is not found in bank {bank.Name} {bank.Id}");
    }
}