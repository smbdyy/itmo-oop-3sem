using Banks.Console.UserInteractionInterfaces;
using Banks.Interfaces;
using Banks.Models;
using Banks.Tools.Exceptions;

namespace Banks.Console.AccountMenuCommandHandlers;

public class SendMoneyCommandHandler : AccountMenuCommandHandler
{
    private readonly ICentralBank _centralBank;

    public SendMoneyCommandHandler(IUserInteractionInterface interactionInterface, ICentralBank centralBank)
        : base(interactionInterface) => _centralBank = centralBank;

    public override bool Handle(string command)
    {
        if (command != "send") return base.Handle(command);

        var recipients = GetPossibleRecipients().ToList();
        if (recipients.Count == 0)
        {
            InteractionInterface.WriteLine("no possible recipients");
            return true;
        }

        for (int i = 0; i < recipients.Count; i++)
        {
            InteractionInterface.WriteLine(
                $"{i}. client {recipients[i].Client.Name.AsString}, account id {recipients[i].Id}");
        }

        InteractionInterface.WriteLine("enter recipient number:");
        int number = UserInputParser.GetIntInRange(0, recipients.Count, InteractionInterface);
        IBankAccount recipient = recipients[number];

        InteractionInterface.WriteLine("enter money amount:");
        MoneyAmount amount = UserInputParser.GetUnsignedDecimal(InteractionInterface);

        try
        {
            Account!.Send(amount, recipient);
            InteractionInterface.WriteLine("success");
        }
        catch (TransactionValidationException ex)
        {
            InteractionInterface.WriteLine($"transaction failed: {ex.Message}");
        }

        return true;
    }

    private IEnumerable<IBankAccount> GetPossibleRecipients()
    {
        var accounts = new List<IBankAccount>();
        foreach (IBank bank in _centralBank.Banks)
        {
            accounts.AddRange(bank.Accounts.Where(account => account != Account));
        }

        return accounts;
    }
}