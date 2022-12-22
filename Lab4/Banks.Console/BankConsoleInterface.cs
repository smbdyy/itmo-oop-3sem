using Banks.Entities;
using Banks.Interfaces;
using Banks.Models;
using Banks.Tools.Exceptions;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Console;

public class BankConsoleInterface
{
    private readonly MainConsoleInterface _mainConsoleInterface;
    private readonly ICentralBank _centralBank;

    public BankConsoleInterface(MainConsoleInterface mainConsoleInterface, IBank bank)
    {
        _mainConsoleInterface = mainConsoleInterface;
        _centralBank = mainConsoleInterface.CentralBank;
        Bank = bank;
    }

    public IBank Bank { get; }

    public void Start()
    {
        System.Console.WriteLine(
            $@"managing bank {Bank.Name} {Bank.Id}, commands:
            exit - go back to main menu
            info - write bank info
            del - delete bank
            set_term - set deposit account term
            set_cred_c - set credit account commission
            set_cred_l - set credit account limit
            set_unv_l - set unverified client withdrawal limit
            add_pair - add start amount -- deposit account percent pair
            sub - subscribe client to bank notifications
            unsub - unsubscribe client from bank notifications
            create_dep - create deposit account
            create_deb - create debit account
            create_cred - create credit account
            list_acc - list all accounts
            select_acc - select account to manage");

        while (true)
        {
            string input = Utils.GetStringInput();
            switch (input)
            {
                case "exit":
                    System.Console.WriteLine("returning to main menu");
                    return;
                case "info":
                    WriteInfo();
                    break;
                case "del":
                    _centralBank.DeleteBank(Bank);
                    System.Console.WriteLine("bank has been deleted, returning to main menu");
                    return;
                case "set_term":
                    SetDepositAccountTerm();
                    break;
                case "set_cred_c":
                    SetCreditAccountCommission();
                    break;
                case "set_cred_l":
                    SetCreditAccountLimit();
                    break;
                case "set_unv_l":
                    SetUnverifiedClientWithdrawalLimit();
                    break;
                case "sub":
                    SubscribeClient();
                    break;
                case "unsub":
                    UnsubscribeClient();
                    break;
                case "add_pair":
                    AddPair();
                    break;
                case "create_deb":
                    CreateDebitAccount();
                    break;
                case "create_dep":
                    CreateDepositAccount();
                    break;
                case "create_cred":
                    CreateCreditAccount();
                    break;
                case "list_acc":
                    WriteAccountsList();
                    break;
                case "select_acc":

                    break;
                default:
                    System.Console.WriteLine("incorrect input");
                    break;
            }
        }
    }

    private IBankAccount GetAccountByInputNumber()
    {
        var accounts = Bank.Accounts.ToList();
        while (true)
        {
            int number = Utils.GetIntInput();
            if (number >= 0 && number < accounts.Count)
            {
                return accounts[number];
            }

            System.Console.WriteLine("incorrect input");
        }
    }

    private void WriteAccountsList()
    {
        if (Bank.Accounts.Count == 0)
        {
            System.Console.WriteLine("no accounts found");
            return;
        }

        var accounts = Bank.Accounts.ToList();
        for (int i = 0; i < accounts.Count; i++)
        {
            System.Console.WriteLine($"{i}. Client: {accounts[i].Client.Name.AsString}, account id: {accounts[i].Id}");
        }
    }

    private void CreateCreditAccount()
    {
        if (_centralBank.Clients.Count == 0)
        {
            System.Console.WriteLine("no clients found");
            return;
        }

        System.Console.WriteLine("choose client");
        _mainConsoleInterface.WriteClientsList();
        BankClient client = _mainConsoleInterface.GetClientByInputNumber();
        Bank.CreateCreditAccount(client);
        System.Console.WriteLine("credit account has been created");
    }

    private void SelectAccount()
    {
        if (Bank.Accounts.Count == 0)
        {
            System.Console.WriteLine("no account found");
            return;
        }

        WriteAccountsList();
        IBankAccount account = GetAccountByInputNumber();
        IEnumerable<IBankAccount> recipients = _mainConsoleInterface.GetAllAccountsExceptGiven(account);
        var accountInterface = new AccountConsoleInterface(account, Bank, recipients);
        accountInterface.Start();
    }

    private void CreateDebitAccount()
    {
        if (_centralBank.Clients.Count == 0)
        {
            System.Console.WriteLine("no clients found");
            return;
        }

        System.Console.WriteLine("choose client:");
        _mainConsoleInterface.WriteClientsList();
        BankClient client = _mainConsoleInterface.GetClientByInputNumber();
        Bank.CreateDebitAccount(client);
        System.Console.WriteLine("debit account has been created");
    }

    private void CreateDepositAccount()
    {
        if (_centralBank.Clients.Count == 0)
        {
            System.Console.WriteLine("no clients found");
            return;
        }

        System.Console.WriteLine("choose client:");
        _mainConsoleInterface.WriteClientsList();
        BankClient client = _mainConsoleInterface.GetClientByInputNumber();
        System.Console.WriteLine("input start money amount");
        while (true)
        {
            try
            {
                decimal startAmount = Utils.GetDecimalInput();
                Bank.CreateDepositAccount(client, startAmount);
                System.Console.WriteLine("deposit account has been created");
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"incorrect input: {ex.Message}");
            }
        }
    }

    private void AddPair()
    {
        while (true)
        {
            System.Console.WriteLine("input start amount:");
            decimal amount = Utils.GetDecimalInput();
            System.Console.WriteLine("input percent:");
            decimal percent = Utils.GetDecimalInput();
            try
            {
                var pair = new StartAmountPercentPair(amount, percent);
                Bank.AddDepositAccountPercent(pair);
                System.Console.WriteLine("pair has been added");
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"incorrect input: {ex.Message}");
            }
            catch (AlreadyExistsException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    private void UnsubscribeClient()
    {
        if (Bank.Subscribers.Count == 0)
        {
            System.Console.WriteLine("no subscribers found");
            return;
        }

        WriteSubscribersList();
        Bank.UnsubscribeFromNotifications(GetSubscriberByInputNumber());
        System.Console.WriteLine("client has been unsubscribed");
    }

    private void WriteSubscribersList()
    {
        var subscribers = Bank.Subscribers.ToList();
        for (int i = 0; i < subscribers.Count; i++)
        {
            System.Console.WriteLine($"{i}. {subscribers[i].Name.AsString} {subscribers[i].Id}");
        }
    }

    private BankClient GetSubscriberByInputNumber()
    {
        var subscribers = Bank.Subscribers.ToList();
        while (true)
        {
            int number = Utils.GetIntInput();
            if (number >= 0 && number < subscribers.Count)
            {
                return subscribers[number];
            }

            System.Console.WriteLine("incorrect input");
        }
    }

    private void SubscribeClient()
    {
        System.Console.WriteLine("choose client:");
        _mainConsoleInterface.WriteClientsList();
        BankClient client = _mainConsoleInterface.GetClientByInputNumber();
        Bank.SubscribeToNotifications(client);
        System.Console.WriteLine("client has been subscribed");
    }

    private void WriteInfo()
    {
        System.Console.WriteLine($"Id: {Bank.Id}");
        System.Console.WriteLine($"Name: {Bank.Name}");
        System.Console.WriteLine($"Current dage: {Bank.CurrentDate}");
        System.Console.WriteLine($"Deposit account term: {Bank.DepositAccountTerm}");
        System.Console.WriteLine($"Credit commission: {Bank.CreditAccountCommission}");
        System.Console.WriteLine($"Credit account limit: {Bank.CreditAccountLimit}");
        System.Console.WriteLine($"Unverified client withdrawal limit: {Bank.UnverifiedClientWithdrawalLimit}");
        if (Bank.StartAmountPercentPairs.Count > 0)
        {
            System.Console.WriteLine("Start amount -- percent pairs:");
            foreach (StartAmountPercentPair pair in Bank.StartAmountPercentPairs)
            {
                System.Console.WriteLine($"amount: {pair.StartAmount}, percent: {pair.Percent}");
            }
        }
    }

    private void SetDepositAccountTerm()
    {
        while (true)
        {
            try
            {
                Bank.DepositAccountTerm = Utils.GetIntInput();
                System.Console.WriteLine("deposit account term has been set");
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"incorrect input: {ex.Message}");
            }
        }
    }

    private void SetCreditAccountCommission()
    {
        while (true)
        {
            try
            {
                Bank.CreditAccountCommission = Utils.GetDecimalInput();
                System.Console.WriteLine("credit account commission has been set");
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"incorrect input: {ex.Message}");
            }
        }
    }

    private void SetCreditAccountLimit()
    {
        while (true)
        {
            try
            {
                Bank.CreditAccountLimit = Utils.GetDecimalInput();
                System.Console.WriteLine("credit account limit has been set");
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"incorrect input: {ex.Message}");
            }
        }
    }

    private void SetUnverifiedClientWithdrawalLimit()
    {
        while (true)
        {
            try
            {
                Bank.UnverifiedClientWithdrawalLimit = Utils.GetDecimalInput();
                System.Console.WriteLine("unverified client withdrawal limit has been set");
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"incorrect input: {ex.Message}");
            }
        }
    }
}