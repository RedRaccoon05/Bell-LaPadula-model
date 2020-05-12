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
        public static string Send_Data(string message)
        {
            try
            {
                byte[] messageFrombase64 = Encoding.UTF8.GetBytes(message);
                message = Convert.ToBase64String(messageFrombase64);
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


                // закрываем сокет
                //  socket.Shutdown(SocketShutdown.Both);
                //   socket.Close();
                message = builder.ToString();
                messageFrombase64 = Convert.FromBase64String(message);
                message = Encoding.UTF8.GetString(messageFrombase64);
                return message;
            }
            catch (SocketException ex)
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
