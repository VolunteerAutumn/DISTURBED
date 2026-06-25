using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp1
{
    internal class Client
    {
        static void Main()
        {
            UdpClient client = new UdpClient();

            IPEndPoint server =
                new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);

            Send(client, server, "JOIN");

            Console.WriteLine("Connected to server.");

            while (true)
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                string msg =
                    Encoding.UTF8.GetString(client.Receive(ref ep));

                Console.WriteLine(msg);

                if (msg == "GAME_START")
                    break;
            }

            for (int round = 1; round <= 5; round++)
            {
                string move;

                do
                {
                    Console.WriteLine();
                    Console.WriteLine($"Round {round}");
                    Console.WriteLine("ROCK / PAPER / SCISSORS");
                    move = Console.ReadLine().ToUpper();
                }
                while (move != "ROCK" &&
                       move != "PAPER" &&
                       move != "SCISSORS");

                Send(client, server, move);

                IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                string result =
                    Encoding.UTF8.GetString(client.Receive(ref ep));

                Console.WriteLine(result);
            }

            IPEndPoint finalEP = new IPEndPoint(IPAddress.Any, 0);
            string finalResult =
                Encoding.UTF8.GetString(client.Receive(ref finalEP));

            Console.WriteLine();
            Console.WriteLine(finalResult);

            client.Close();

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        static void Send(UdpClient client, IPEndPoint server, string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            client.Send(data, data.Length, server);
        }
    }
}
