using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace ClientSide
{
    static class SendData
    {
        static int port = 1337;
        static string address = "127.0.0.1";
    public  static  string Send_Data(string message)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                Console.Write("Введите сообщение:");
              
                byte[] data = Encoding.UTF8.GetBytes(message);
                socket.Send(data);

                // получаем ответ
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                Console.WriteLine("ответ сервера: " + builder.ToString());
        
                // закрываем сокет
                //  socket.Shutdown(SocketShutdown.Both);
                //   socket.Close();
                return builder.ToString();
            }
            catch(SocketException ex)
            {
                if (ex.ErrorCode == 10061)
                    return "Ошибка. Соединение не установленно!";
                else return ex.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
