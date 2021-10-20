﻿using PacketData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Client
{
    class Client
    {
        private TcpClient socket;
        private NetworkStream stream;
        private Encryption clientEncryption = new Encryption();

        string serverPublicKeyPath = Directory.GetCurrentDirectory() + "\\server-public-key.txt";
        private Encryption serverEncryption = new Encryption();

        public Client()
        {
            // generate client key
            clientEncryption.GenerateKey();

            // load server public key
            LoadServerPublicKey();
        }

        public void LoadServerPublicKey()
        {
            if (File.Exists(serverPublicKeyPath))
            {
                string serverPublicKeyLoaded = File.ReadAllText(serverPublicKeyPath);
                serverEncryption.publicKey = serverEncryption.ConvertStringToKey(serverPublicKeyLoaded);
            }
        }

        public void Connect(string ip, int port)
        {
            try
            {
                // try connect to server
                socket = new TcpClient(ip, port);
                stream = socket.GetStream();
                Console.WriteLine("Connected to server...");

                stream.BeginRead(Constant.dataBuffer, 0, Constant.dataBuffer.Length, ReceiveData, null);

                // send client public key to server
                string clientPublicKeyOnString = clientEncryption.ConvertKeyToString(clientEncryption.publicKey);
                string encryptedKey = serverEncryption.Encrypt(clientPublicKeyOnString);
                SendData(Packet.SEND_KEY, encryptedKey);

                Console.WriteLine($"Sending Client Public Key to Server...");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e);
            }
        }

        private void ReceiveData(IAsyncResult _result)
        {
            try
            {
                int _byteLength = stream.EndRead(_result);
                if (_byteLength <= 0)
                {
                    // disconnected
                    return;
                }

                byte[] data = new byte[_byteLength];
                Array.Copy(Constant.dataBuffer, data, _byteLength);

                HandleData(data);
                stream.BeginRead(Constant.dataBuffer, 0, Constant.dataBuffer.Length, ReceiveData, null);
            }
            catch (Exception _ex)
            {
                Console.WriteLine($"Error receiving TCP data: {_ex}");
                // disconnected
            }
        }

        private void HandleData(byte[] data)
        {
            byte[] buffer = data;
            int readPos = 0;

            int packetType = BitConverter.ToInt32(buffer, readPos);
            readPos += 4;

            switch (packetType)
            {
                case (int)Packet.SEND_KEY:
                    string keyString = BitConverter.ToString(buffer, readPos);
                    break;
                case (int)Packet.SEND_MESSAGE:
                    string message = BitConverter.ToString(buffer, readPos);
                    Console.WriteLine($"Client:\n{message}");
                    break;
                default:
                    break;
            }
        }

        public void SendData(Packet packet, string data)
        {
            // convert data to byte
            List<byte> dataToSend = new List<byte>();
            dataToSend.AddRange(BitConverter.GetBytes((int)packet));
            dataToSend.AddRange(Encoding.ASCII.GetBytes(data));

            // send to server
            stream.Write(dataToSend.ToArray(), 0, dataToSend.Count);
        }

        public void SendData(Packet packet, byte[] data)
        {
            // convert data to byte
            List<byte> dataToSend = new List<byte>();
            dataToSend.AddRange(BitConverter.GetBytes((int)packet));
            dataToSend.AddRange(data);

            // send to server
            stream.Write(dataToSend.ToArray(), 0, dataToSend.Count);
        }
    }
}