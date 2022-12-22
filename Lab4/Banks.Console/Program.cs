using Banks.Builders;
using Banks.Console;
using Banks.Entities;

var centralBank = new CentralBank(new DefaultBankBuilder());
var mainInterface = new MainConsoleInterface(centralBank);
mainInterface.Start();