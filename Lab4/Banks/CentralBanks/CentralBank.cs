using Banks.Banks;
using Banks.Banks.Builders;
using Banks.Clients;
using Banks.Tools.Exceptions;

namespace Banks.CentralBanks;

public class CentralBank : ICentralBank
{
    private readonly List<BankClient> _clients = new ();
    private readonly List<IBank> _banks = new ();

    public CentralBank(DateOnly currentDate)
    {
        CurrentDate = currentDate;
    }

    public DateOnly CurrentDate { get; private set; }

    public IReadOnlyCollection<BankClient> Clients => _clients;
    public IReadOnlyCollection<IBank> Banks => _banks;

    public IBank CreateBank(BankBuilder builder)
    {
        IBank bank = builder.Build();
        _banks.Add(bank);
        return bank;
    }

    public void DeleteBank(IBank bank)
    {
        if (!_banks.Contains(bank))
        {
            throw NotFoundException.Bank(bank);
        }

        _banks.Remove(bank);
    }

    public void RegisterClient(BankClient client)
    {
        if (_clients.Contains(client))
        {
            throw AlreadyExistsException.ClientRegistered(client);
        }

        _clients.Add(client);
    }

    public void NotifyNextDay()
    {
        CurrentDate = CurrentDate.AddDays(1);
        foreach (IBank bank in _banks)
        {
            bank.NotifyNextDay();
        }
    }

    public void DeleteClientAndAccounts(BankClient client)
    {
        if (!_clients.Contains(client))
        {
            throw NotFoundException.BankClient(client);
        }

        foreach (IBank bank in _banks)
        {
            bank.DeleteAllClientAccounts(client);
        }

        _clients.Remove(client);
    }
}