using BankExcercise.Transactions;
using System;

namespace BankExcercise.Accounts.Checking
{
public class IndividualAccount : CheckingAccount
{
    public IndividualAccount(int owner, decimal balance) : base(owner, balance)
    {
    }

    public override bool Withdrawl(decimal withdrawlAmount)
    {
        if (balance < withdrawlAmount)
        {
            throw new ApplicationException("insufficient funds");
        }
        else if(withdrawlAmount > 1000)
        {
            throw new ApplicationException("withdrawl limit exceeded");
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
}
}
