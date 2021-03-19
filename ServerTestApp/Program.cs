using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ServerTestApp
{
    class ServerTest
    {

        static int port = 8005;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            IPEndPoint IpPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"),port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listenSocket.Bind(IpPoint);
                listenSocket.Listen(10);
                Console.WriteLine("Сервер запущен, ожидание подключений...");               
                Socket handler = listenSocket.Accept();
                StringBuilder builder = new StringBuilder();
                int bytes = 0;
                byte[] data = new byte[256];
                Console.WriteLine("Подключение установлено");
                while (true)
                {
                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0,bytes));
                    } while (handler.Available>0);

                    if (builder.ToString()=="0")
                    {
                        handler.Shutdown(SocketShutdown.Both);//остановка как отправки, так и получения данных сокетом
                        handler.Close();
                        Console.WriteLine("Удаленный хост принудительно разорвал существующее подключение.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now.ToShortTimeString() + ":" + builder.ToString());
                        builder.Clear();
                        string message = "Сообщение доставлено";
                        data = Encoding.Unicode.GetBytes(message);
                        handler.Send(data);
                    }
                    
                }
                
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            
            
        }
    }
}
