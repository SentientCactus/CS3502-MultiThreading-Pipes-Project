using System;
using System.Threading;

//Serves as the base custom class that stores all the info and all base level logic methods
public class BankAccount {
    //varibles athat are used within the class along with it's specefied lock
    private int balance;
    public string accountName;
    private readonly Mutex locky = new Mutex();

    //Initalizer
    public BankAccount (int intBalance, string name) {
        balance = intBalance;
        accountName = name;
    }

    //The only get function bexuase I frogot to set name to private and ended up coding all the logic when I realized it
    public int getBalance() {
        return balance;
    }

    //Deposits into the account with no regard to the lock
    public void Deposit (int amount, string threadName) {
        balance += amount;
        Console.WriteLine("Thread " + threadName + " deposited " + amount + " into acount " + accountName + "\nNew balance: " + balance);
    }

    //Withdraws from the account with no regard to the lock
    public void Withdraw (int amount, string threadName) {
        if (balance > amount) {
            balance -= amount;
            Console.WriteLine("Thread " + threadName + " withdrew " + amount + " from account " + accountName + "\nNew balance: " + balance);
        } else {
            Console.WriteLine("Insufficent funds");
        }
    }

    //Deposits into the account, but requires lock access before doing so
    public void MutexDeposit (int amount, string threadName) {
        Console.WriteLine("Thread " + threadName + " is trying to access account " + accountName);

        locky.WaitOne();

        //Is within a try/finally block to make sure the lock is realsed in case of an error, this is the case for all lock request
        //I will only highlight the first one per file
        try {
            Deposit(amount, threadName);
        } finally {
            Console.WriteLine("Thread " + threadName + " is releasing account " + accountName);
            locky.ReleaseMutex();
        }
    }
    
    //Same as MutexDeposit except that it calls the withdraw method instead
    public void MutexWithdraw (int amount, string threadName) {
        Console.WriteLine("Thread " + threadName + " is trying to access account " + accountName);

        locky.WaitOne();

        try {
            Withdraw(amount, threadName);
        } finally {
            Console.WriteLine("Thread " + threadName + " is releasing account " + accountName);
            locky.ReleaseMutex();
        }
    }

    //Transfer methoed that doesn't use deadlock prevention calls both the MutexWithdraw function from this account and MutexDeposit from the target account
    //Why is this one here and the other one within TransactionHandler? It just kinda ended up that way.
    public void Transfer (int amount, string threadName, BankAccount target) {
        Console.WriteLine("Thread " + threadName + " is trying to transfer funds from account " + accountName + " to account " + target.accountName);

        if (amount < balance) {
            MutexWithdraw(amount, threadName);
            target.MutexDeposit(amount, threadName);
        } else {
            Console.WriteLine("Insufficent funds");
        }
    }

    //Used within the external transfer function that has proper ordering and timeout prevetion
    public bool TryLock() {
        //Speaking of timeour prevention, it's right here
        return locky.WaitOne(250);
    }

    //Used to unlock the mutexs from the outside
    public void Unlock() {
        locky.ReleaseMutex();
    }
}