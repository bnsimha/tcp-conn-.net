using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTCPClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string serverIpAddress = "127.0.0.1"; // Replace with your server's IP address
            int serverPort = 13000; // Replace with your server's port number

            await ConnectToServerAsync(serverIpAddress, serverPort);
        }

        static async Task ConnectToServerAsync(string serverIp, int serverPort)
        {
            try
            {
                TcpClient client = new TcpClient(serverIp, serverPort);
                Console.WriteLine($"Connected to server {serverIp}:{serverPort}");

                NetworkStream stream = client.GetStream();

                // Send messages to the server
                for (int i = 0; i < 3; i++)
                {
                    string message = $"Message {i + 1} from client";
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    await stream.WriteAsync(data, 0, data.Length);
                    Console.WriteLine($"Sent to server: {message}");

                    // Receive response from the server
                    byte[] responseBuffer = new byte[4096];
                    int bytesRead = await stream.ReadAsync(responseBuffer, 0, responseBuffer.Length);
                    string response = Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);
                    Console.WriteLine($"Received from server: {response}");

                    Thread.Sleep(1000); // Simulate delay between messages
                }

                stream.Close();
                client.Close();
                Console.WriteLine("Connection closed.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e}");
            }
        }
    }
}
