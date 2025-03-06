using System;
using System.IO;

public class Reader {
    //A progarm that simply looks and waits to recive messages from a predertimied
    //pipe once all msgs have been read it will stop running
    public static void Main(string[] args) {
        string genPipePath = "/tmp/genpipe";

        using (StreamReader reade = new StreamReader(genPipePath)) {
            string msg;
            while ((msg = reade.ReadLine()) != null) {
                Console.WriteLine("Received: " + msg);
            }
        }
    }
}