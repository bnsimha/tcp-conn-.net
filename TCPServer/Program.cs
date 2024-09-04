// // See https://aka.ms/new-console-template for more information

// using TCPServer;

// namespace TCPServer
// {
//     class Program{
//         static void Main(string[] args){

//             Thread serverThread=new Thread(()=> new Server("127.0.0.1",13000));
//             serverThread.Start();
//             Console.WriteLine("Server started ....!");
//         }

//     }
// }

using TCPServer;

class Program
{
    static void Main(string[] args)
    {
        string ipAddress = "127.0.0.1";
        int port = 13000;
        Server server = new Server(ipAddress, port);
        Console.WriteLine("Server started. Press Enter to exit...");
        Console.ReadLine();
    }
}
