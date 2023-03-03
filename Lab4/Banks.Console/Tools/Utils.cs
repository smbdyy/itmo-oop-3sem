using Banks.Accounts;
using Banks.Banks;
using Banks.CentralBanks;
using Banks.Clients;
using Banks.Console.UserInteractionInterfaces;

namespace Banks.Console.Tools;

public static class Utils
{
    public static IBankAccount GetAccountByInputNumber(IBank bank, IUserInteractionInterface interactionInterface)
    {
        WriteAccountsList(bank, interactionInterface);
        interactionInterface.WriteLine("enter account number:");

        var accounts = bank.Accounts.ToList();
        return accounts[UserInputParser.GetIntInRange(0, accounts.Count, interactionInterface)];
    }

    public static IBank GetBankByInputNumber(
        ICentralBank centralBank,
        IUserInteractionInterface interactionInterface)
    {
        WriteBanksList(centralBank, interactionInterface);
        interactionInterface.WriteLine("enter bank number:");

        var banks = centralBank.Banks.ToList();
        return banks[UserInputParser.GetIntInRange(0, banks.Count, interactionInterface)];
    }

    public static BankClient GetClientByInputNumber(
        ICentralBank centralBank,
        IUserInteractionInterface interactionInterface)
    {
        WriteClientsList(centralBank, interactionInterface);
        interactionInterface.WriteLine("enter client number:");

        var clients = centralBank.Clients.ToList();
        return clients[UserInputParser.GetIntInRange(0, clients.Count, interactionInterface)];
    }

    public static void WriteClientsList(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
    {
        if (centralBank.Clients.Count == 0)
        {
            interactionInterface.WriteLine("no clients found");
            return;
        }

        var clients = centralBank.Clients.ToList();
        for (int i = 0; i < clients.Count; i++)
        {
            interactionInterface.WriteLine($"{i}. {clients[i].Name.AsString}, id {clients[i].Id}");
        }
    }

    public static void WriteBanksList(ICentralBank centralBank, IUserInteractionInterface interactionInterface)
    {
        if (centralBank.Banks.Count == 0)
        {
            interactionInterface.WriteLine("no banks found");
        }

        var banks = centralBank.Banks.ToList();
        for (int i = 0; i < banks.Count; i++)
        {
            interactionInterface.WriteLine($"{i}. {banks[i].Name}, id {banks[i].Id}");
        }
    }

    public static void WriteAccountsList(IBank bank, IUserInteractionInterface interactionInterface)
    {
        if (bank.Accounts.Count == 0)
        {
            interactionInterface.WriteLine("no accounts found");
            return;
        }

        var accounts = bank.Accounts.ToList();
        for (int i = 0; i < accounts.Count; i++)
        {
            interactionInterface.WriteLine($"{i}. Client: {accounts[i].Client.Name.AsString}, account id: {accounts[i].Id}");
        }
    }
}