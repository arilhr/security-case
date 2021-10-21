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

            //Encryption enc = new Encryption();
            //AesEncryptor aes = new AesEncryptor();

            //enc.GenerateKey();
            //aes.GenerateNewKey();
            //string key = Convert.ToBase64String(aes.aes.Key);
            //string IV = Convert.ToBase64String(aes.aes.IV);

            //string encrypted = enc.Encrypt(key);
            //Console.WriteLine($"Encrypted: {encrypted}||");
            //string decrypted = enc.Decrypt(encrypted);
            //Console.WriteLine($"Decrypted: {decrypted}||");

            Console.ReadKey();
        }
    }
}
