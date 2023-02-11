using System.Net.Sockets;

namespace MagPie_Home_Automation_RESTful_API_Server.Controllers.MyRoom.Computer
{
    public enum ControlTypes
    {
        ComputerControls,
        MediaControls
    }

    public class TCPSocketClient
    {
        private const string _serverAddress = "192.168.1.150";
        private const int _port = 8082;
        private const int _portMedia = 8083;

        public static string? SendCommand(string command, ControlTypes controlType)
        {
            try
            {
                int port;
                switch (controlType)
                {
                    case ControlTypes.ComputerControls:
                        port = _port;
                        break;

                    case ControlTypes.MediaControls:
                        port = _portMedia;
                        break;

                    default:
                        throw new ArgumentException("Bad controlType argument");
                }

                using var client = new TcpClient(_serverAddress, port);

                byte[] data = System.Text.Encoding.UTF8.GetBytes(command);

                using var stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                Console.WriteLine($"[COMPUTER TCPSOCKETCLIENT] Data send: {command}");

                data = new byte[1024];
                string responseData = string.Empty;

                int bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.UTF8.GetString(data, 0, bytes);
                Console.WriteLine($"[COMPUTER TCPSOCKETCLIENT] Data received: {responseData}");

                return responseData;
            }
            catch (Exception e)
            {
                Console.WriteLine($"[COMPUTER TCPSOCKETCLIENT] Exception caught: {e}");
                return null;
            }
        }
    }
}
