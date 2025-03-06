using System;
using System.Threading;

//Handles most of the thread's logic with each methoed corosping to a certain phase and I will say which one at each methoed
//Also does have the transfer methoed that implments ordering and timeout
//All of the thread logic methods work similar way to one another. They all choose a how many actions the thread will do, what type of action that will be and how much money will be subjected
public class TransactionHandler {

    //The method that handles phase One's thread logic, only has two action types that can occur
    public static void Transactions(BankAccount bankAccount) {
        Random randy = new Random();

        //Each thread will always preform between 0 and 10 actions and will move between 0 and 1000 dollars for each action
        int i = 0;
        int loops = randy.Next(11);
        while (i < loops) {
            int action = randy.Next(2);
            int amount = randy.Next(1001);

            if (action == 0) {
                bankAccount.Deposit(amount, Environment.CurrentManagedThreadId.ToString());
            } else {
                bankAccount.Withdraw(amount, Environment.CurrentManagedThreadId.ToString());
            }
            i++;
        }
    }

    //The method that handles thread logic in phase two, does the exact same thing as above, just calls the mutex variants of the functions
    public static void MutexTransactions(BankAccount bankAccount) {
        Random randy = new Random();

        int i = 0;
        int loops = randy.Next(11);
        while (i < loops) {
            int action = randy.Next(2);
            int amount = randy.Next(1001);

            if (action == 0) {
                bankAccount.MutexDeposit(amount, Environment.CurrentManagedThreadId.ToString());
            } else {
                bankAccount.MutexWithdraw(amount, Environment.CurrentManagedThreadId.ToString());
            }
            i++;
        }
    }

    //Handles the thread logic for phase three, there is now six possible actions states and it uses a switch statemnt instead of two if statements
    public static void Transactions(BankAccount accountOne, BankAccount accountTwo) {
        Random randy = new Random();

        //Still the same 0 to 10 action and 0 to 1000 dollars
        int i = 0;
        int loops = randy.Next(11);
        while (i < loops) {
            int action = randy.Next(6);
            int amount = randy.Next(1001);

            switch (action) {
                case 0:
                    accountOne.MutexDeposit(amount, Environment.CurrentManagedThreadId.ToString());
                    break;
                case 1:
                    accountOne.MutexWithdraw(amount, Environment.CurrentManagedThreadId.ToString());
                    break;
                case 2:
                    accountTwo.MutexWithdraw(amount, Environment.CurrentManagedThreadId.ToString());
                    break;
                case 3:
                    accountTwo.MutexWithdraw(amount, Environment.CurrentManagedThreadId.ToString());
                    break;
                case 4:
                    accountOne.Transfer(amount, Environment.CurrentManagedThreadId.ToString(), accountTwo);
                    break;
                case 5:
                    accountTwo.Transfer(amount, Environment.CurrentManagedThreadId.ToString(), accountOne);
                    break;
            }
            i++;
        }
    }

    //Handles the thread logic of phase 4, apart from that the code is almost the same as above, just calls a diffrent transfer method
    public static void DeadlocklessTransactions (BankAccount accountOne, BankAccount accountTwo) {
        Random randy = new Random();

        int i = 0;
        int loops = randy.Next(11);
        while (i < loops) {
            int action = randy.Next(6);
            int amount = randy.Next(1001);

            switch (action) {
                case 0:
                    accountOne.MutexDeposit(amount, Environment.CurrentManagedThreadId.ToString());
                    break;
                case 1:
                    accountOne.MutexWithdraw(amount, Environment.CurrentManagedThreadId.ToString());
                    break;
                case 2:
                    accountTwo.MutexWithdraw(amount, Environment.CurrentManagedThreadId.ToString());
                    break;
                case 3:
                    accountTwo.MutexWithdraw(amount, Environment.CurrentManagedThreadId.ToString());
                    break;
                case 4:
                    AntiDeadlockTransfer(amount, Environment.CurrentManagedThreadId.ToString(), accountOne, accountTwo);
                    break;
                case 5:
                    AntiDeadlockTransfer(amount, Environment.CurrentManagedThreadId.ToString(), accountTwo, accountOne);
                    break;
            }
            i++;
        }
    }

    //Similar logic to the transfer method within the BankAccount class, but properly orders then and has a specfied wait time when it trys to obtain the locks
    public static void AntiDeadlockTransfer (int amount, string threadName, BankAccount accountOne, BankAccount accountTwo) {
        Console.WriteLine("Thread " + threadName + " is trying to transfer funds from account " + accountOne.accountName + " to account " + accountTwo.accountName);
        
        //Declares and properly orders the lock based upon thier alphebtical names
        BankAccount first = accountOne;
        BankAccount second = accountTwo;
        if (string.CompareOrdinal(accountOne.accountName, accountTwo.accountName) > 0) {
            first = accountTwo;
            second = accountOne;
        }

        //booleans used to see if the thread has aquired both locks
        bool lockOne = false;
        bool lockTwo = false;

        //The try/finally statement that handles the transfer logic and makes sure the locks can be unlocked in the event of faliure
        try {
            Console.WriteLine("Thread " + threadName + " is trying to access account " + accountOne.accountName);
            lockOne = first.TryLock();
            Console.WriteLine("Thread " + threadName + " is trying to access account " + accountTwo.accountName);
            lockTwo = second.TryLock();

            //Checks to see if it has aquired both locks
            if (lockOne && lockTwo) {
                if (amount < accountOne.getBalance()) {
                    accountOne.Withdraw(amount, threadName);
                    accountTwo.Deposit(amount, threadName);
                    Console.WriteLine("Transfered " + amount + " from account " + accountOne.accountName + " to account " + accountTwo.accountName);
                } else {
                    Console.WriteLine("Insufficent Funds");
                }
            } else {
                Console.WriteLine("Unable to aqcuire both locks");
            }
        } finally {

            //Simply relases each lock if the thread has just one or both
            if (lockTwo) {
                Console.WriteLine("Thread " + threadName + " is releasing account " + second.accountName);
                second.Unlock();
            }
            if (lockOne) {
                Console.WriteLine("Thread " + threadName + " is releasing account " + first.accountName);
                first.Unlock();
            }
        }
    }
}