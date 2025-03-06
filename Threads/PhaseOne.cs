using System;
using System.Threading;

//Creates a single bankacount and asks the user how many threads they want that will then run a method from TransactionHandler
public static class PhaseOne {
    public static void Run() {
        int choice;
        List<Thread> threads = new List<Thread>();
        BankAccount bankAccount = new BankAccount(10000, "One");
        Console.WriteLine("\nRunning Phase One\nHow many threads do you want:");

        //Makes sure the users input is greater than 0 and actually a number
        while (true) {
            int.TryParse(Console.ReadLine(), out choice);

            if (0 < choice) {
                break;
            } else {
                Console.WriteLine("Input a valid number greater than 0:");
            }
        }

        //Populates the list and assigns the corpsoing method
        int i = 0;
        while (i < choice) {
            Thread thread = new Thread(() => TransactionHandler.Transactions(bankAccount));
            threads.Add(thread);
            thread.Start();
            i++;
        }

        //Starts the magic
        foreach (var t in threads) {
            t.Join();
        }

        Console.WriteLine("\nFinal Balance: " + bankAccount.getBalance() + "\nPhase One Finished");
    }
}