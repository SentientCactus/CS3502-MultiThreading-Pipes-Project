using System;
using System.Threading;

//Mutex implemeation of phase one
public static class PhaseTwo {
    public static void Run() {
        int choice;
        List<Thread> threads = new List<Thread>();
        BankAccount bankAccount = new BankAccount(10000, "One");
        Console.WriteLine("\nRunning Phase Two\nHow many threads do you want:");

        while (true) {
            int.TryParse(Console.ReadLine(), out choice);

            if (0 < choice) {
                break;
            } else {
                Console.WriteLine("Input a valid number greater than 0:");
            }
        }

        //The only diffrence is what method the threads are handed
        int i = 0;
        while (i < choice) {
            Thread thread = new Thread(() => TransactionHandler.MutexTransactions(bankAccount));
            threads.Add(thread);
            thread.Start();
            i++;
        }

        foreach (var t in threads) {
            t.Join();
        }

        Console.WriteLine("\nFinal Balance: " + bankAccount.getBalance() + "\nPhase Two Finished");
    }
}