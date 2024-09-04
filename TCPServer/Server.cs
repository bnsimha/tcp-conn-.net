using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPServer
{
    public class Server
    {
        private static object _lock = new object();
        private readonly TcpListener server;
        private int clientCount = 1;
        private static Dictionary<int, TcpClient> clients = new Dictionary<int, TcpClient>();

        public Server(string ip, int port)
        {
            IPAddress localAddress = IPAddress.Parse(ip);
            server = new TcpListener(localAddress, port);
            server.Start();
            StartListener();
        }

        public async void StartListener()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for client connections...");
                    TcpClient client = await server.AcceptTcpClientAsync();

                    lock (_lock)
                    {
                        clients.Add(clientCount, client);
                    }
                    Console.WriteLine($"Client {clientCount} is connected and added to dictionary");

                    Task.Run(() => HandleClientConnection(clientCount));

                    clientCount++;
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine($"SocketException: {e}");
                server.Stop();
            }
        }

        public async Task HandleClientConnection(int clientId)
        {
            TcpClient client;
            lock (_lock)
            {
                client = clients[clientId];
            }

            try
            {
                while (true)
                {
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int byteCount = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (byteCount == 0)
                    {
                        break;
                    }

                    string data = Encoding.ASCII.GetString(buffer, 0, byteCount);
                    Broadcast(data);
                    Console.WriteLine($"Client {clientId} sent: {data}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception in client {clientId}: {e}");
            }
            finally
            {
                lock (_lock)
                {
                    clients.Remove(clientId);
                }
                client.Close();
                Console.WriteLine($"Client {clientId} disconnected.");
            }
        }

        public static void Broadcast(string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message + Environment.NewLine);

            lock (_lock)
            {
                foreach (TcpClient client in clients.Values)
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
