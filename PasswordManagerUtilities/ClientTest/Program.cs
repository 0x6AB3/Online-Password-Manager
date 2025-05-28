using System;
using System.Threading;
using PasswordManagerUtilities;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting server");
            Server newServer = new Server("127.0.0.1", 12043);
            Thread serverThread = new Thread(new ParameterizedThreadStart(newServer.Begin));
            serverThread.Start();
            /*
            IPAddress[] serverIPs = Dns.GetHostAddresses(Dns.GetHostName());
            IPAddress serverIP = null;

            for (int i = 0; i < serverIPs.Length; i++)
            {
                if (serverIPs[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    serverIP = serverIPs[i];
                }
            }
            */


            Console.WriteLine("Starting client");

            Client newClient = new Client("127.0.0.1", 12043);
            Thread t = new Thread(newClient.Begin);
            t.Start();

            Console.ReadLine();
        }
    }
}
