using System.Net;
using System.Net.Sockets;
using System.Text;
namespace ExampleMenu.Model
{
    static class DataSocket
    {
        static int port = 1337;
        static string address = "127.0.0.1";
        static IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
        static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static public void SendtoServer(string message)
        {
            socket.Connect(iPEndPoint);
            byte[] messageByte = Encoding.Default.GetBytes(message);
            socket.Send(messageByte);
        }
        static public string ReceivetoServer()
        {
            byte[] data = new byte[256];
            StringBuilder message = new StringBuilder();
            do
            {
                int bytes = socket.Receive(data, data.Length, 0);
                message.Append(Encoding.Default.GetString(data, 0, bytes));
            } while (socket.Available > 0);
            return message.ToString();
        }
    }
}
