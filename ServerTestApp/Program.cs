using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerTestApp
{
    class Program
    {
        static int port = 8005;
        static void Main(string[] args)
        {
            IPEndPoint IpPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listenSocket.Bind(IpPoint);
                listenSocket.Listen(10);
                Console.WriteLine("Сервер запущен, ожидание подключений...");

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Hello World!");
        }
    }
}
