using System;
using System.Threading;

//Uses two bank accounts to allow transfers, this phase has no deadlock prevention, so it can lock up
public static class PhaseThree {
    public static void Run() {
        int choice;
        List<Thread> threads = new List<Thread>();
        BankAccount accountOne = new BankAccount(10000, "One");
        BankAccount accountTwo = new BankAccount(10000, "Two");
        Console.WriteLine("\nRunning Phase Three\nHow many threads do you want:");

        while (true) {
            int.TryParse(Console.ReadLine(), out choice);

            if (0 < choice) {
                break;
            } else {
                Console.WriteLine("Input a valid number greater than 0:");
            }
        }

        //passes yet another diffrent method
        int i = 0;
        while (i < choice) {
            Thread thread = new Thread(() => TransactionHandler.Transactions(accountOne, accountTwo));
            threads.Add(thread);
            thread.Start();
            i++;
        }

        foreach (var t in threads) {
            t.Join();
        }

        Console.WriteLine("\nFinal Balance Account One: " + accountOne.getBalance() + "\nFinal Balance Account Two: " + accountTwo.getBalance() + "\nPhase Three Finished");
    }
}