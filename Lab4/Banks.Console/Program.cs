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

var accountCreationContext = new AccountCreationContext();
AccountCreationCommandHandler accountCreationChain =
    new SetClientCommandHandler(centralBank, interactionInterface, accountCreationContext);

var selectAccountTypeContext = new SelectAccountTypeContext();
SelectAccountTypeCommandHandler selectAccountTypeChain = new SelectCreditAccountHandler(interactionInterface, selectAccountTypeContext);
selectAccountTypeChain
    .SetNext(new SelectDebitAccountHandler(interactionInterface, selectAccountTypeContext))
    .SetNext(new SelectDepositAccountHandler(interactionInterface, selectAccountTypeContext));
selectAccountTypeChain.SetAccountCreationChain(accountCreationChain);

var accountMenuContext = new AccountMenuContext();
AccountMenuCommandHandler accountMenuChain = new AccountInfoCommandHandler(interactionInterface, accountMenuContext);
accountMenuChain
    .SetNext(new ExitAccountMenuCommandHandler(interactionInterface, accountMenuContext))
    .SetNext(new ReplenishCommandHandler(interactionInterface, accountMenuContext))
    .SetNext(new SendMoneyCommandHandler(interactionInterface, accountMenuContext, centralBank))
    .SetNext(new UndoCommandHandler(interactionInterface, accountMenuContext))
    .SetNext(new WithdrawCommandHandler(interactionInterface, accountMenuContext))
    .SetNext(new WriteHistoryCommandHandler(interactionInterface, accountMenuContext));

var bankMenuContext = new BankMenuContext();
BankMenuCommandHandler bankMenuChain = new AddPairCommandHandler(interactionInterface, bankMenuContext);
bankMenuChain
    .SetNext(new CreateAccountCommandHandler(interactionInterface, selectAccountTypeChain, centralBank, bankMenuContext))
    .SetNext(new ExitBankMenuCommandHandler(interactionInterface, bankMenuContext))
    .SetNext(new BankInfoCommandHandler(interactionInterface, bankMenuContext))
    .SetNext(new ListAccountsCommandHandler(interactionInterface, bankMenuContext))
    .SetNext(new SelectAccountCommandHandler(interactionInterface, accountMenuChain, bankMenuContext))
    .SetNext(new SetCommissionCommandHandler(interactionInterface, bankMenuContext))
    .SetNext(new SetCreditLimitCommandHandler(interactionInterface, bankMenuContext))
    .SetNext(new SetTermCommandHandler(interactionInterface, bankMenuContext))
    .SetNext(new SetUnverifiedClientLimitHandler(interactionInterface, bankMenuContext))
    .SetNext(new SubscribeCommandHandler(interactionInterface, centralBank, bankMenuContext))
    .SetNext(new UnsubscribeCommandHandler(interactionInterface, bankMenuContext));

var clientMenuContext = new ClientMenuContext();
ClientMenuCommandHandler clientMenuChain = new ClientInfoCommandHandler(interactionInterface, clientMenuContext);
clientMenuChain
    .SetNext(new ExitClientMenuCommandHandler(interactionInterface, clientMenuContext))
    .SetNext(new SetAddressCommandHandler(interactionInterface, addressCreationChain, clientMenuContext))
    .SetNext(new SetPassportNumberCommandHandler(interactionInterface, clientMenuContext));

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