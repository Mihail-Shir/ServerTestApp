using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerTestApp
{
    class ClientSocket
    {
        static int port = 8005;
        static string adress = "127.0.0.1";
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(adress), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);
                Console.WriteLine("Соединение установлено. Для отключение отправьте: 0");
                while (true)
                {
                    byte[] data;
                    Console.WriteLine("Введите сообщение:");
                    string message = Console.ReadLine();
                    if (message==""||message==null)
                    {
                        message = "Пусто";
                    }
                    else if (message=="0")
                    {
                        data = Encoding.Unicode.GetBytes(message);
                        socket.Send(data);
                        Console.WriteLine("Вы разорвали соединение!");
                        break;
                    }
                    data = Encoding.Unicode.GetBytes(message);
                    socket.Send(data);

                    int bytes = 0;
                    data = new byte[256];
                    StringBuilder builder = new StringBuilder();

                    do
                    {
                        bytes = socket.Receive(data, data.Length, 0);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));

                    } while (socket.Available > 0);
                    if (builder.ToString()!=null)
                    {
                        Console.WriteLine("Сервер: " + builder.ToString());
                    }
                
                };
                //socket.Shutdown(SocketShutdown.Both);
                //socket.Close();

            }
            catch (Exception ex)
            {                                
                Console.WriteLine(ex.Message);
            }
            Console.Read();
            
        }
    }
}
