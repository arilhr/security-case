using PacketData;
using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 7777;
            Client client = new Client();

            client.Connect(ip, port);

            Console.ReadKey();
        }
    }
}
