﻿using Banks.Builders;
using Banks.Console;
using Banks.Console.AccountCreationCommandHandlers;
using Banks.Console.AccountMenuCommandHandlers;
using Banks.Console.AddressCreationCommandHandlers;
using Banks.Console.BankCreationHandlers;
using Banks.Console.BankMenuCommandHandlers;
using Banks.Console.ClientCreationCommandHandlers;
using Banks.Console.ClientMenuCommandHandlers;
using Banks.Console.MainMenuCommandHandlers;
using Banks.Console.Tools;
using Banks.Console.UserInteractionInterfaces;
using Banks.Entities;
using Banks.Tools.NotificationReceivers;

var centralBank = new CentralBank(new DefaultBankBuilder());
var interactionInterface = new ConsoleUserInteractionInterface();

var addressBuilder = new AddressBuilder();
AddressCreationCommandHandler addressCreationChain = new SetCountryHandler(addressBuilder, interactionInterface);
addressCreationChain
    .SetNext(new SetTownHandler(addressBuilder, interactionInterface))
    .SetNext(new SetStreetHandler(addressBuilder, interactionInterface))
    .SetNext(new SetHouseNumberHandler(addressBuilder, interactionInterface));

BankCreationCommandHandler bankCreationChain = new SetNameHandler(centralBank, interactionInterface);
bankCreationChain
    .SetNext(new SetCreditAccountCommissionHandler(centralBank, interactionInterface))
    .SetNext(new SetCreditAccountLimitHandler(centralBank, interactionInterface))
    .SetNext(new SetDepositAccountTermHandler(centralBank, interactionInterface))
    .SetNext(new SetUnverifiedClientWithdrawalLimitHandler(centralBank, interactionInterface));

BankClientBuilder clientBuilder = new BankClientBuilder().AddNotificationReceiver(new ConsoleNotificationReceiver());
ClientCreationCommandHandler clientCreationChain = new SetClientNameHandler(clientBuilder, interactionInterface);
clientCreationChain
    .SetNext(new SetAddressHandler(clientBuilder, interactionInterface, addressCreationChain))
    .SetNext(new SetPassportNumberHandler(clientBuilder, interactionInterface));

AccountCreationCommandHandler accountCreationChain = new SetClientCommandHandler(centralBank, interactionInterface);

SelectAccountTypeCommandHandler selectAccountTypeChain = new SelectCreditAccountHandler(interactionInterface);
selectAccountTypeChain
    .SetNext(new SelectDebitAccountHandler(interactionInterface))
    .SetNext(new SelectDepositAccountHandler(interactionInterface));
selectAccountTypeChain.SetAccountCreationChain(accountCreationChain);

AccountMenuCommandHandler accountMenuChain = new AccountInfoCommandHandler(interactionInterface);
accountMenuChain
    .SetNext(new ExitAccountMenuCommandHandler(interactionInterface))
    .SetNext(new ReplenishCommandHandler(interactionInterface))
    .SetNext(new SendMoneyCommandHandler(interactionInterface, centralBank))
    .SetNext(new UndoCommandHandler(interactionInterface))
    .SetNext(new WithdrawCommandHandler(interactionInterface))
    .SetNext(new WriteHistoryCommandHandler(interactionInterface));

BankMenuCommandHandler bankMenuChain = new AddPairCommandHandler(interactionInterface);
bankMenuChain
    .SetNext(new CreateAccountCommandHandler(interactionInterface, selectAccountTypeChain, centralBank))
    .SetNext(new ExitBankMenuCommandHandler(interactionInterface))
    .SetNext(new BankInfoCommandHandler(interactionInterface))
    .SetNext(new ListAccountsCommandHandler(interactionInterface))
    .SetNext(new SelectAccountCommandHandler(interactionInterface, accountMenuChain))
    .SetNext(new SetCommissionCommandHandler(interactionInterface))
    .SetNext(new SetCreditLimitCommandHandler(interactionInterface))
    .SetNext(new SetTermCommandHandler(interactionInterface))
    .SetNext(new SetUnverifiedClientLimitHandler(interactionInterface))
    .SetNext(new SubscribeCommandHandler(interactionInterface, centralBank))
    .SetNext(new UnsubscribeCommandHandler(interactionInterface));

ClientMenuCommandHandler clientMenuChain = new ClientInfoCommandHandler(interactionInterface);
clientMenuChain
    .SetNext(new ExitClientMenuCommandHandler(interactionInterface))
    .SetNext(new SetAddressCommandHandler(interactionInterface, addressCreationChain))
    .SetNext(new SetPassportNumberCommandHandler(interactionInterface));

MainMenuCommandHandler mainMenuChain = new AddDaysCommandHandler(centralBank, interactionInterface);
mainMenuChain
    .SetNext(new CreateBankCommandHandler(interactionInterface, bankCreationChain))
    .SetNext(new CreateClientCommandHandler(centralBank, interactionInterface, clientCreationChain))
    .SetNext(new ExitCommandHandler(interactionInterface))
    .SetNext(new ListBanksCommandHandler(centralBank, interactionInterface))
    .SetNext(new ListClientsCommandHandler(centralBank, interactionInterface))
    .SetNext(new SelectBankCommandHandler(centralBank, interactionInterface, bankMenuChain))
    .SetNext(new SelectClientCommandHandler(interactionInterface, centralBank, clientMenuChain));

while (mainMenuChain.Handle(UserInputParser.GetLine(interactionInterface)))
{
}