﻿using Banks.Console.UserInteractionInterfaces;
using Banks.Models;
using ArgumentException = Banks.Tools.Exceptions.ArgumentException;

namespace Banks.Console.ClientMenuCommandHandlers;

public class SetPassportNumberCommandHandler : ClientMenuCommandHandler
{
    public SetPassportNumberCommandHandler(IUserInteractionInterface interactionInterface)
        : base(interactionInterface) { }

    public override bool Handle(string command)
    {
        if (command == "set_passport") return base.Handle(command);

        while (true)
        {
            try
            {
                Client!.PassportNumber = PassportNumber.FromString(UserInputParser.GetLine(InteractionInterface));
                InteractionInterface.WriteLine("success");
                return true;
            }
            catch (ArgumentException ex)
            {
                InteractionInterface.WriteLine($"incorrect input: {ex.Message}");
            }
        }
    }
}