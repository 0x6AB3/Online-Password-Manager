using System;
using System.Threading;
using PasswordManagerUtilities;
using System.Net;

namespace ServerSoftware
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip;
            int port;
            
            // IP address input and validation.
            while (true)
            {
                Console.Write("Server IP (Default = 127.0.0.1): ");
                ip = Console.ReadLine();

                // Default IP (localhost).
                if (ip == "")
                {
                    ip = "127.0.0.1";
                    break;
                }

                try
                {
                    IPAddress.Parse(ip);
                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid IP, please use an IPv4 address (xxx.xxx.xxx.xxx)");
                }
            }

            // Port input and validation.
            while (true)
            {
                Console.Write("Server Port (Default = 9600): ");

                try
                {
                    string portInput = Console.ReadLine();

                    if (portInput == "")
                    {
                        port = 9600;
                        break;
                    }

                    port = int.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Invalid Port, please use an integer (e.g: 5120)");
                }
            }

            // Beginning to listen for connections from clients.
            try
            {
                Server server = new Server("127.0.0.1", 9600, "VaultDatabase.mdb");
                Thread t = new Thread(server.Begin);
                t.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Server failed to start, error message: {e.Message}\nPress [ENTER] to exit");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("[STATUS] Server running");
            bool running = true;
            DateTime start = DateTime.Now;

            // Main runtime loop, allowing the server administrator to basic commands and statistics about the server status.
            while (running)
            {
                switch (Console.ReadLine())
                {
                    default:
                        Console.WriteLine("Invalid command, use /help for a list of commands");
                        break;

                    case "/help":
                        Console.WriteLine(
                            "\n/help\tShows the list of valid commands\n" +
                            "/time\tDisplays server runtime (seconds)\n" +
                            "/abort\tShuts down the server\n" +
                            "/clear\tClears the console screen\n");
                        break;

                    case "/time":
                        Console.WriteLine((DateTime.Now - start).TotalSeconds);
                        break;

                    case "/abort":
                        Environment.Exit(0);
                        return;

                    case "/clear":
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
