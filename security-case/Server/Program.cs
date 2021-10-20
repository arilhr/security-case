using PacketData;
using System;
using System.Security.Cryptography;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 7777;
            Server server = new Server(ip, port);

            server.Start();

            Console.ReadKey();
        }
    }
}
