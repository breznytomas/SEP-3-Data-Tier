using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CarSharing_Database.Mediator
{
    public class Server
    {
        public int Port { get; set; } = 2910;
        public string Hostname { get; set; } = "127.0.0.1";

        private bool _isRunning;
        private readonly TcpListener _listener;

        public Server()
        {
            var ipAddress = IPAddress.Parse(Hostname);
            _listener = new TcpListener(ipAddress, Port);
            _isRunning = false;
        }

        public void Run()
        {
            _listener.Start();
            Console.WriteLine("Database Tier started...");

            _isRunning = true;
            while (_isRunning)
            {
                TcpClient client = _listener.AcceptTcpClient();
                Console.WriteLine($"Client {((IPEndPoint)client.Client.RemoteEndPoint)?.Address} connected...");

                new Thread(
                    new ClientHandler(client).Run
                    ).Start();
            }
        }
    }
}