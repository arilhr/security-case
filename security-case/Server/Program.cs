using PacketData;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 7777);

            server.Start();

            Console.ReadKey();
        }
    }
}
