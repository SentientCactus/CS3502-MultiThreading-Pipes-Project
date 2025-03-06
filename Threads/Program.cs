using System;
using System.Transactions;

//A simple senteinal loop that run each phase, this is more of an organizational thing. The same can be said about the phase classes as they don't
//handle much logic apart from creating threads, declaring needed varibles, and serveing as a way to organize the code.
//The two files that handle most of the logic are BankAccount.cs and TransactionHandler.cs
public class Program {
    public static void Main(string[] args) {
        while (true) {
            Console.WriteLine("\nMulti-Threaded Banking System\n1. Phase 1: Basic Threading\n2. Phase 2: Resoutce Protection\n3. Phase 3: Deadlock Creation\n4. Phase 4: Deadlock Prevention\n5. Exit Progarm");
            Console.WriteLine("Enter your selection:");
            string choice = Console.ReadLine();

            //Each phase works in a fundematlly similar way where it creates all the varubles it needs, then asks the user how many threads
            //they want and has each thread run a corrsopsing methoed within TransactionHandler, so similar code will only be explained within
            //the first phase since not much changes and it would add reduant comments
            switch (choice) {
                //Creates a single bankaccount and has threads preform unprotected actions, can have a negative account amount
                case "1":
                    PhaseOne.Run();
                    break;

                //Same as phase one, but utalizes the Mutex with the bankaccount class, can't have a negactive account amount
                case "2":
                    PhaseTwo.Run();
                    break;

                //Has two bankaccounts that use protected actions, transfering between two accounts has no deadlock prevention
                case "3":
                    PhaseThree.Run();
                    break;

                //Same as phase three, but the transfer action has proper ordering and timeout implemenations
                case "4":
                    PhaseFour.Run();
                    break;

                //exits the program
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Choice.");
                    break;
            }
        }
    }
}