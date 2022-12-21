using Banks.Builders;
using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;
using Banks.Tools.Exceptions;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Console;

public class ConsoleClientInterface
{
    private ICentralBank _centralBank;
    private BankClient _client;

    public ConsoleClientInterface(ICentralBank centralBank, BankClient client)
    {
        if (!centralBank.Clients.Contains(client))
        {
            throw NotFoundException.BankClient(client);
        }

        _centralBank = centralBank;
        _client = client;
    }

    public ConsoleClientInterface(ICentralBank centralBank, Guid clientId)
    {
        BankClient? client = centralBank.Clients.FirstOrDefault(c => c.Id == clientId);

        _centralBank = centralBank;
        _client = client ?? throw NotFoundException.BankClientById(clientId);
    }

    public void SetPassportNumber()
    {
        while (true)
        {
            try
            {
                _client.PassportNumber = PassportNumber.FromString(Utils.GetStringInput());
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    public void Init()
    {
        System.Console.WriteLine(
            $@"managing client {_client.Name.AsString} {_client.Id}, commands:
            exit - go back to main menu
            del - delete client and all their accounts
            set_a - set address
            set_p - set passport number");

        while (true)
        {
            string input = Utils.GetStringInput();
            switch (input)
            {
                case "exit":
                    System.Console.WriteLine("returning to main menu");
                    return;
                case "del":
                    _centralBank.DeleteClientAndAccounts(_client);
                    System.Console.WriteLine("client deleted, returning to main menu");
                    return;
                case "set_a":
                    var addressBuilder = new ConsoleAddressBuilder(new AddressBuilder());
                    addressBuilder.InputAllData();
                    _client.Address = addressBuilder.Build();
                    System.Console.WriteLine("address has been set");
                    break;
                case "set_p":
                    SetPassportNumber();
                    System.Console.WriteLine("passport number has been set");
                    break;
            }
        }
    }
}