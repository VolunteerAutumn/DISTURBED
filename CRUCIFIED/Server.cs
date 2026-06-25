using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp1
{
    internal class Server
    {
        static void Main()
        {
            UdpClient server = new UdpClient(5000);

            Console.WriteLine("Server started on port 5000");
            Console.WriteLine("Waiting for players...");

            IPEndPoint player1 = null;
            IPEndPoint player2 = null;

            while (player1 == null || player2 == null)
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                string msg = Encoding.UTF8.GetString(server.Receive(ref ep));

                if (msg == "JOIN")
                {
                    if (player1 == null)
                    {
                        player1 = ep;
                        Console.WriteLine("Player 1 connected");
                        Send(server, player1, "You are Player 1");
                    }
                    else if (player2 == null)
                    {
                        player2 = ep;
                        Console.WriteLine("Player 2 connected");
                        Send(server, player2, "You are Player 2");
                    }
                }
            }

            Send(server, player1, "GAME_START");
            Send(server, player2, "GAME_START");

            int score1 = 0;
            int score2 = 0;

            for (int round = 1; round <= 5; round++)
            {
                Console.WriteLine($"Round {round}");

                string move1 = null;
                string move2 = null;

                while (move1 == null || move2 == null)
                {
                    IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                    byte[] data = server.Receive(ref ep);

                    string move = Encoding.UTF8.GetString(data);

                    if (ep.Address.Equals(player1.Address) &&
                        ep.Port == player1.Port)
                    {
                        move1 = move;
                    }
                    else if (ep.Address.Equals(player2.Address) &&
                             ep.Port == player2.Port)
                    {
                        move2 = move;
                    }
                }

                int winner = DetermineWinner(move1, move2);

                string result1;
                string result2;

                if (winner == 0)
                {
                    result1 = result2 = "DRAW";
                }
                else if (winner == 1)
                {
                    score1++;
                    result1 = "WIN";
                    result2 = "LOSE";
                }
                else
                {
                    score2++;
                    result1 = "LOSE";
                    result2 = "WIN";
                }

                Send(server, player1,
                    $"ROUND {round}: {result1}\nScore: {score1}-{score2}");

                Send(server, player2,
                    $"ROUND {round}: {result2}\nScore: {score2}-{score1}");
            }

            string final1;
            string final2;

            if (score1 > score2)
            {
                final1 = "GAME WON!";
                final2 = "GAME LOST!";
            }
            else if (score2 > score1)
            {
                final1 = "GAME LOST!";
                final2 = "GAME WON!";
            }
            else
            {
                final1 = final2 = "GAME DRAW!";
            }

            Send(server, player1, final1);
            Send(server, player2, final2);

            Console.WriteLine("Game finished.");

            server.Close();
        }

        static void Send(UdpClient server, IPEndPoint ep, string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            server.Send(data, data.Length, ep);
        }

        static string ReceiveFrom(UdpClient server, IPEndPoint expected)
        {
            while (true)
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = server.Receive(ref ep);

                if (ep.Address.Equals(expected.Address) &&
                    ep.Port == expected.Port)
                {
                    return Encoding.UTF8.GetString(data);
                }
            }
        }

        static int DetermineWinner(string p1, string p2)
        {
            if (p1 == p2)
                return 0;

            if ((p1 == "ROCK" && p2 == "SCISSORS") ||
                (p1 == "SCISSORS" && p2 == "PAPER") ||
                (p1 == "PAPER" && p2 == "ROCK"))
                return 1;

            return 2;
        }
    }
}
