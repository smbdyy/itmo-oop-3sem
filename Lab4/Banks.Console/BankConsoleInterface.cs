using Banks.Interfaces;

namespace Banks.Console;

public class BankConsoleInterface
{
    private ICentralBank _centralBank;
    private IBank _bank;

    public BankConsoleInterface(ICentralBank centralBank, IBank bank)
    {
        _centralBank = centralBank;
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
            set_unv_l - set unverified client withdrawal limit");

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
                default:
                    System.Console.WriteLine("incorrect input");
                    break;
            }
        }
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
}