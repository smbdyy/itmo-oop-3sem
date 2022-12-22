using Banks.Builders;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Console;

public class MainConsoleInterface
{
    private ICentralBank _centralBank;
    private CentralBankConsoleInterface _centralBankInterface;

    public MainConsoleInterface(ICentralBank centralBank)
    {
        _centralBank = centralBank;
        _centralBankInterface = new CentralBankConsoleInterface(_centralBank);
    }

    public void Start()
    {
        System.Console.WriteLine(
            @"initialized, commands:
                exit
                create_b - create new bank
                select_b - select bank to manage
                create_c - create client
                select_c - select client to manage
                next_day - notify banks about next day
        ");

        while (true)
        {
            string input = Utils.GetStringInput();
            switch (input)
            {
                case "exit":
                    return;
                case "create_b":
                    CreateBank();
                    break;
                case "create_c":
                    CreateClient();
                    break;
                case "select_c":
                    SelectClient();
                    break;
                case "select_b":
                    SelectBank();
                    break;
                case "next_day":
                    _centralBank.NotifyNextDay();
                    System.Console.WriteLine($"success, current date: {_centralBank.CurrentDate.ToString()}");
                    break;
                default:
                    System.Console.WriteLine("incorrect input");
                    break;
            }
        }
    }

    private void CreateBank()
    {
        _centralBankInterface.InputAllBankData();
        IBank bank = _centralBankInterface.InputNameCreateBank();
        System.Console.WriteLine($"bank created, id: {bank.Id}");
    }

    private void CreateClient()
    {
        var builder = new ConsoleClientBuilder(_centralBank, new BankClientBuilder());
        builder.InputAllData();
        BankClient client = builder.Build();
        System.Console.WriteLine($"client created, id: {client.Id}");
    }

    private void SelectClient()
    {
        if (_centralBank.Clients.Count == 0)
        {
            System.Console.WriteLine("no clients found");
            return;
        }

        WriteClientsList();
        BankClient selectedClient = GetClientByInputNumber();
        var clientInterface = new ClientConsoleInterface(_centralBank, selectedClient);
        clientInterface.Start();
    }

    private void SelectBank()
    {
        if (_centralBank.Clients.Count == 0)
        {
            System.Console.WriteLine("no banks found");
            return;
        }

        WriteBanksList();
        IBank bank = GetBankByInputNumber();
        var bankInterface = new BankConsoleInterface(_centralBank, bank);
        bankInterface.Start();
    }

    private void WriteClientsList()
    {
        var clients = _centralBank.Clients.ToList();
        for (int i = 0; i < clients.Count; i++)
        {
            System.Console.WriteLine($"{i}. {clients[i].Name.AsString}, id {clients[i].Id}");
        }
    }

    private BankClient GetClientByInputNumber()
    {
        var clients = _centralBank.Clients.ToList();
        System.Console.WriteLine("input a number:");
        while (true)
        {
            int number = Utils.GetIntInput();
            if (number >= 0 && number < clients.Count)
            {
                return clients[number];
            }

            System.Console.WriteLine("incorrect input");
        }
    }

    private void WriteBanksList()
    {
        var banks = _centralBank.Banks.ToList();
        for (int i = 0; i < banks.Count; i++)
        {
            System.Console.WriteLine($"{i}. {banks[i].Name}, id {banks[i].Id}");
        }
    }

    private IBank GetBankByInputNumber()
    {
        var banks = _centralBank.Banks.ToList();
        System.Console.WriteLine("input a number:");
        while (true)
        {
            int number = Utils.GetIntInput();
            if (number >= 0 && number < banks.Count)
            {
                return banks[number];
            }

            System.Console.WriteLine("incorrect input");
        }
    }
}