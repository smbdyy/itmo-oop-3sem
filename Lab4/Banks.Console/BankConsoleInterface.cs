﻿using Banks.Entities;
using Banks.Interfaces;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Console;

public class BankConsoleInterface
{
    private MainConsoleInterface _mainConsoleInterface;
    private ICentralBank _centralBank;
    private IBank _bank;

    public BankConsoleInterface(MainConsoleInterface mainConsoleInterface, IBank bank)
    {
        _mainConsoleInterface = mainConsoleInterface;
        _centralBank = mainConsoleInterface.CentralBank;
        _bank = bank;
    }

    public void Start()
    {
        System.Console.WriteLine(
            $@"managing bank {_bank.Name} {_bank.Id}, commands:
            exit - go back to main menu
            info - write bank info
            del - delete bank
            set_term - set deposit account term
            set_cred_c - set credit account commission
            set_cred_l - set credit account limit
            set_unv_l - set unverified client withdrawal limit
            sub - subscribe client to bank notifications");

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
                    _centralBank.DeleteBank(_bank);
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
                default:
                    System.Console.WriteLine("incorrect input");
                    break;
            }
        }
    }

    private void UnsubscribeClient()
    {
        if (_bank.Subscribers.Count == 0)
        {
            System.Console.WriteLine("no subscribers found");
            return;
        }

        WriteSubscribersList();
        _bank.UnsubscribeFromNotifications(GetSubscriberByInputNumber());
        System.Console.WriteLine("client has been unsubscribed");
    }

    private void WriteSubscribersList()
    {
        var subscribers = _bank.Subscribers.ToList();
        for (int i = 0; i < subscribers.Count; i++)
        {
            System.Console.WriteLine($"{i}. {subscribers[i].Name.AsString} {subscribers[i].Id}");
        }
    }

    private BankClient GetSubscriberByInputNumber()
    {
        var subscribers = _bank.Subscribers.ToList();
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
        _bank.SubscribeToNotifications(client);
        System.Console.WriteLine("client has been subscribed");
    }

    private void WriteInfo()
    {
        System.Console.WriteLine($"Id: {_bank.Id}");
        System.Console.WriteLine($"Name: {_bank.Name}");
        System.Console.WriteLine($"Current dage: {_bank.CurrentDate}");
        System.Console.WriteLine($"Deposit account term: {_bank.DepositAccountTerm}");
        System.Console.WriteLine($"Credit commission: {_bank.CreditAccountCommission}");
        System.Console.WriteLine($"Credit account limit: {_bank.CreditAccountLimit}");
        System.Console.WriteLine($"Unverified client withdrawal limit: {_bank.UnverifiedClientWithdrawalLimit}");
    }

    private void SetDepositAccountTerm()
    {
        while (true)
        {
            try
            {
                _bank.DepositAccountTerm = Utils.GetIntInput();
                System.Console.WriteLine("deposit account term has been set");
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    private void SetCreditAccountCommission()
    {
        while (true)
        {
            try
            {
                _bank.CreditAccountCommission = Utils.GetDecimalInput();
                System.Console.WriteLine("credit account commission has been set");
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    private void SetCreditAccountLimit()
    {
        while (true)
        {
            try
            {
                _bank.CreditAccountLimit = Utils.GetDecimalInput();
                System.Console.WriteLine("credit account limit has been set");
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    private void SetUnverifiedClientWithdrawalLimit()
    {
        while (true)
        {
            try
            {
                _bank.UnverifiedClientWithdrawalLimit = Utils.GetDecimalInput();
                System.Console.WriteLine("unverified client withdrawal limit has been set");
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }
}