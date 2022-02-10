using System;
using System.Collections.Generic;
using System.Linq;

namespace BankExcercise
{
public class Bank
{
    public string name;

    private List<Account> bankAccounts;
    public Bank(string name)
    {
        this.name = name;
        bankAccounts = new List<Account>();
    }

    public int OpenBankAccount(Type accountType, decimal startingBalance)
    {
        int newId = bankAccounts.Count();

        bankAccounts.Add((Account)Activator.CreateInstance(accountType, newId, startingBalance));

        return newId;
    }

    public Account GetAccount(int ownerId)
    {
        Account account = bankAccounts.Where(x => x.owner == ownerId).FirstOrDefault();

        if(account == null)
        {
            throw new ApplicationException("no account exists with that id");
        }

        return account;
    }

    public bool TransferFunds(int fromAccountId, int toAccountId, decimal transferAmount)
    {
        if(transferAmount <= 0)
        {
            throw new ApplicationException("transfer amount must be positive");
        }
        else if (transferAmount == 0)
        {
            throw new ApplicationException("invalid transfer amount");
        }

        Account fromAccount = GetAccount(fromAccountId);
        Account toAccount = GetAccount(toAccountId);

        if(fromAccount.balance < transferAmount)
        {
            throw new ApplicationException("insufficient funds");
        }

        fromAccount.Transfer(-1 * transferAmount, toAccountId);
        toAccount.Transfer(transferAmount, fromAccountId);

        return true;
    }
}
}
