using System.Text.RegularExpressions;
using Banks.Builders;
using Banks.Entities;
using Banks.Interfaces;

namespace Banks.Console;

public class MainConsoleInterface
{
    private BankCreatorConsoleInterface _bankCreatorInterface;

    public MainConsoleInterface(ICentralBank centralBank)
    {
        CentralBank = centralBank;
        _bankCreatorInterface = new BankCreatorConsoleInterface(CentralBank);
    }

    public ICentralBank CentralBank { get; }

    public void Start()
    {
        System.Console.WriteLine(
            @"initialized, commands:
                exit
                create_b - create new bank
                select_b - select bank to manage
                create_c - create client
                select_c - select client to manage
                add_days [number] - add [number] days to current date and notify banks
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
                case var _ when Regex.IsMatch(input, "^add_days\\s[1-9][0-9]*$"):
                    AddDays(Convert.ToInt32(input.Split(' ')[1]));
                    break;
                default:
                    System.Console.WriteLine("incorrect input");
                    break;
            }
        }
    }

    public void WriteClientsList()
    {
        var clients = CentralBank.Clients.ToList();
        for (int i = 0; i < clients.Count; i++)
        {
            System.Console.WriteLine($"{i}. {clients[i].Name.AsString}, id {clients[i].Id}");
        }
    }

    public BankClient GetClientByInputNumber()
    {
        var clients = CentralBank.Clients.ToList();
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

    private void AddDays(int number)
    {
        for (int i = 0; i < number; i++)
        {
            CentralBank.NotifyNextDay();
        }

        System.Console.WriteLine($"success, current date: {CentralBank.CurrentDate.ToString()}");
    }

    private void CreateBank()
    {
        _bankCreatorInterface.InputAllBankData();
        IBank bank = _bankCreatorInterface.InputNameCreateBank();
        System.Console.WriteLine($"bank created, id: {bank.Id}");
    }

    private void CreateClient()
    {
        var builder = new ConsoleClientBuilder(CentralBank, new BankClientBuilder());
        builder.InputAllData();
        BankClient client = builder.Build();
        System.Console.WriteLine($"client created, id: {client.Id}");
    }

    private void SelectClient()
    {
        if (CentralBank.Clients.Count == 0)
        {
            System.Console.WriteLine("no clients found");
            return;
        }

        WriteClientsList();
        BankClient selectedClient = GetClientByInputNumber();
        var clientInterface = new ClientConsoleInterface(CentralBank, selectedClient);
        clientInterface.Start();
    }

    private void SelectBank()
    {
        if (CentralBank.Clients.Count == 0)
        {
            System.Console.WriteLine("no banks found");
            return;
        }

        WriteBanksList();
        IBank bank = GetBankByInputNumber();
        var bankInterface = new BankConsoleInterface(this, bank);
        bankInterface.Start();
    }

    private void WriteBanksList()
    {
        var banks = CentralBank.Banks.ToList();
        for (int i = 0; i < banks.Count; i++)
        {
            System.Console.WriteLine($"{i}. {banks[i].Name}, id {banks[i].Id}");
        }
    }

    private IBank GetBankByInputNumber()
    {
        var banks = CentralBank.Banks.ToList();
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