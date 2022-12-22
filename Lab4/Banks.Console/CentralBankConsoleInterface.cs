using Banks.Interfaces;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Console;

public class CentralBankConsoleInterface
{
    private ICentralBank _centralBank;

    public CentralBankConsoleInterface(ICentralBank centralBank)
        => _centralBank = centralBank;

    public void InputAllBankData()
    {
        InputDepositAccountTerm();
        InputCreditAccountCommission();
        InputCreditAccountLimit();
        InputUnverifiedClientWithdrawalLimit();
    }

    public void InputDepositAccountTerm()
    {
        System.Console.WriteLine("input deposit account term:");
        while (true)
        {
            try
            {
                _centralBank.SetDefaultDepositAccountTerm(Utils.GetIntInput());
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    public void InputCreditAccountCommission()
    {
        System.Console.WriteLine("input credit account commission:");
        while (true)
        {
            try
            {
                _centralBank.SetDefaultCreditAccountCommission(Utils.GetDecimalInput());
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    public void InputCreditAccountLimit()
    {
        System.Console.WriteLine("input credit account limit:");
        while (true)
        {
            try
            {
                _centralBank.SetDefaultCreditAccountLimit(Utils.GetDecimalInput());
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    public void InputUnverifiedClientWithdrawalLimit()
    {
        System.Console.WriteLine("input unverified client withdrawal limit");
        while (true)
        {
            try
            {
                _centralBank.SetDefaultUnverifiedClientWithdrawalLimit(Utils.GetDecimalInput());
                return;
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }

    public IBank InputNameCreateBank()
    {
        System.Console.WriteLine("input bank name");
        while (true)
        {
            try
            {
                return _centralBank.CreateBank(Utils.GetStringInput());
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine($"exception: {ex.Message}");
            }
        }
    }
}