using Banks.Interfaces;
using Banks.Tools.Exceptions;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Console;

public class AccountConsoleInterface
{
    private readonly IBankAccount _account;
    private readonly IBank _bank;
    private readonly List<IBankAccount> _recipients;

    public AccountConsoleInterface(IBankAccount account, IBank bank, IEnumerable<IBankAccount> recipients)
    {
        _account = account;
        _bank = bank;
        _recipients = recipients.ToList();
    }

    public void Start()
    {
        System.Console.WriteLine(
            @"managing selected account, commands:
            exit - go back to bank menu
            info - write account info
            del  - delete account
            with - withdraw money
            repl - replenish money
            send - send money
            hist - write transactions history
            undo - undo transaction");

        while (true)
        {
            string input = Utils.GetStringInput();
            switch (input)
            {
                case "exit":
                    System.Console.WriteLine("returning to bank menu");
                    return;
                case "del":
                    _bank.DeleteAccount(_account);
                    System.Console.WriteLine("account deleted, returning to bank menu");
                    return;
                case "info":
                    WriteAccountInfo();
                    break;
                case "with":
                    Withdraw();
                    break;
                case "repl":
                    Replenish();
                    break;
                case "send":
                    Send();
                    break;
                case "hist":
                    WriteTransactionHistory();
                    break;
                default:
                    System.Console.WriteLine("incorrect input");
                    break;
            }
        }
    }

    private void Send()
    {
        while (true)
        {
            System.Console.WriteLine("enter amount:");
            decimal amount = Utils.GetDecimalInput();
            System.Console.WriteLine("choose recipient:");
            WriteRecipients();
            IBankAccount recipient = GetRecipientByInputNumber();
            try
            {
                _account.Send(amount, recipient);
                System.Console.WriteLine(_account.TransactionHistory.Last().Description);
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"incorrect input: {ex.Message}");
            }
            catch (TransactionValidationException ex)
            {
                System.Console.WriteLine($"transaction failed: {ex.Message}");
            }
        }
    }

    private void Replenish()
    {
        System.Console.WriteLine("enter amount:");
        while (true)
        {
            decimal amount = Utils.GetDecimalInput();
            try
            {
                _account.Replenish(amount);
                System.Console.WriteLine(_account.TransactionHistory.Last().Description);
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"incorrect input: {ex.Message}");
            }
            catch (TransactionValidationException ex)
            {
                System.Console.WriteLine($"transaction failed: {ex.Message}");
            }
        }
    }

    private void Withdraw()
    {
        System.Console.WriteLine("enter amount:");
        while (true)
        {
            decimal amount = Utils.GetDecimalInput();
            try
            {
                _account.Withdraw(amount);
                System.Console.WriteLine(_account.TransactionHistory.Last().Description);
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"incorrect input: {ex.Message}");
            }
            catch (TransactionValidationException ex)
            {
                System.Console.WriteLine($"transaction failed: {ex.Message}");
            }
        }
    }

    private IBankAccount GetRecipientByInputNumber()
    {
        System.Console.WriteLine("enter number:");
        while (true)
        {
            int number = Utils.GetIntInput();
            if (number >= 0 && number < _recipients.Count)
            {
                return _recipients[number];
            }

            System.Console.WriteLine("incorrect input");
        }
    }

    private void WriteRecipients()
    {
        if (_recipients.Count == 0)
        {
            System.Console.WriteLine("no recipients found");
            return;
        }

        for (int i = 0; i < _recipients.Count; i++)
        {
            System.Console.WriteLine($"{i}. {_recipients[i].Client.Name.AsString} account {_recipients[i].Id}");
        }
    }

    private void WriteAccountInfo()
    {
        System.Console.WriteLine($"Id: {_account.Id}");
        System.Console.WriteLine($"Client: {_account.Client.Name.AsString}, id {_account.Client.Id}");
        System.Console.WriteLine($"Bank: {_bank.Name}, id {_bank.Id}");
        System.Console.WriteLine($"Money amount: {_account.MoneyAmount}");
    }

    private void WriteTransactionHistory()
    {
        if (_account.TransactionHistory.Count == 0)
        {
            System.Console.WriteLine("no transactions found");
            return;
        }

        var transactions = _account.TransactionHistory.ToList();
        for (int i = 0; i < transactions.Count; i++)
        {
            System.Console.WriteLine($"{i}. {transactions[i].Description}");
        }
    }
}