using System;
using System.IO;

public class Writer {
    //A progarm that will send messages to a preditemined pipe, it has a simple menu
    //to help the user guide it's function. I it will not stop unless told to.
    public static void Main(string[] args) {
        string genPipePath = "/tmp/genpipe";

        using (StreamWriter writy = new StreamWriter(genPipePath)) {
            writy.AutoFlush = true;
            while (true) {
                Console.WriteLine("\nMessenger\n1. Send a message\n2. Send a message X times\n3. Exit");
                string choice = Console.ReadLine();
                string msg;

                switch (choice) {
                    //Sends one msg
                    case "1":
                        Console.WriteLine("Input message:");
                        msg = Console.ReadLine();

                        Console.WriteLine("Sending MSG");
                        writy.WriteLine(msg);
                        break;

                    //Sends one msg X times
                    case "2":
                        Console.WriteLine("Input message:");
                        msg = Console.ReadLine();

                        Console.WriteLine("Input loops:");
                        if (!int.TryParse(Console.ReadLine(), out int max)) {
                            Console.WriteLine("Invalid number.");
                            break;
                        }

                        Console.WriteLine("Sending MSGs");
                        for (int i = 0; i < max; i++) {
                            writy.WriteLine(msg);
                        }
                        break;

                    //Exits the progarm
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}