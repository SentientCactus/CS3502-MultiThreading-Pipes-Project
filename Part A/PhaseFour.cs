using System;
using System.Threading;

//Same as phase three, just uses coded that has proper deadlock prevention and avoidannce
public static class PhaseFour {
    public static void Run() {
        int choice;
        List<Thread> threads = new List<Thread>();
        BankAccount accountOne = new BankAccount(10000, "One");
        BankAccount accountTwo = new BankAccount(10000, "Two");
        Console.WriteLine("\nRunning Phase Four\nHow many threads do you want:");

        while (true) {
            int.TryParse(Console.ReadLine(), out choice);

            if (0 < choice) {
                break;
            } else {
                Console.WriteLine("Input a valid number greater than 0:");
            }
        }

        //Uses yet agian, a unquie methoed
        int i = 0;
        while (i < choice) {
            Thread thread = new Thread(() => TransactionHandler.DeadlocklessTransactions(accountOne, accountTwo));
            threads.Add(thread);
            thread.Start();
            i++;
        }

        foreach (var t in threads) {
            t.Join();
        }

        Console.WriteLine("\nFinal Balance Account One: " + accountOne.getBalance() + "\nFinal Balance Account Two: " + accountTwo.getBalance() + "\nPhase Four Finished");
    }
}