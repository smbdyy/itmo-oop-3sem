﻿using Banks.Entities;

namespace Banks.Interfaces;

public interface IBankAccount
{
    public BankClient Client { get; }
    public decimal MoneyAmount { get; }
    public decimal MinMoneyAmount { get; }

    public void SetState(IAccountState state);
    public decimal Withdraw(decimal amount);
    public void Replenish(decimal amount);
    public void Send(decimal amount, IBankAccount recipient);
    public void Receive(TransferTransaction transaction);
    public void Undo(Guid transactionId);
}