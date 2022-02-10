using BankExcercise.Transactions;
using System;
using System.Collections.Generic;

namespace BankExcercise
{
public abstract class Account
{
    public int owner { get; set; }

    public decimal balance { get; set; }

    public List<Transaction> transactions { get; set; }

    public Account(int owner, decimal balance)
    {
        this.owner = owner;
        this.balance = balance;
        transactions = new List<Transaction>();
    }

    public virtual bool Withdrawl(decimal withdrawlAmount)
    {
        if (balance < withdrawlAmount)
        {
            throw new ApplicationException("insufficient funds");
        }
        else if (withdrawlAmount <= 0)
        {
            throw new ApplicationException("invalid withdrawl amount");
        }

        balance -= withdrawlAmount;

        Transaction newTransaction = new WithdrawlTransaction(withdrawlAmount);
        transactions.Add(newTransaction);

        return true;
    }

    public void Transfer(decimal transferAmount, int transferToId)
    {
        balance += transferAmount;
        TransferTransaction newTransaction = new TransferTransaction(transferAmount, transferToId, owner);
        transactions.Add(newTransaction);
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ApplicationException("invalid deposit amount");
        }

        balance += amount;

        Transaction newTransaction = new DepositTransaction(amount);
        transactions.Add(newTransaction);
    }
}
}
