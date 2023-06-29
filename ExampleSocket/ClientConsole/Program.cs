using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //наш сервер, який буде отримувать повідомлення
            IPAddress serverIP = IPAddress.Parse("91.238.103.135");
            int port = 9076;
            try
            {
                //ідентифікація сервер ip+port
                IPEndPoint endPoint = new IPEndPoint(serverIP, port);
                Socket client = new Socket(AddressFamily.InterNetwork, 
                    SocketType.Stream, ProtocolType.Tcp);
                //підключення до сервера
                client.Connect(endPoint);
                Console.Write("Вкажіть повідомлення->_");
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                client.Send(data); //відправляємо повідомелння в байтах
                //очікуємо відповіді від сервера
                data = new byte[1024];
                int bytes = 0; //розмір повідомлення від сервера
                do
                {
                    bytes = client.Receive(data); //отримує інформацію від сервера
                    Console.WriteLine("Нам приалав сервер {0}", Encoding.Unicode.GetString(data));

                } while (client.Available > 0);
                client.Shutdown(SocketShutdown.Both); //завершуємо зяднання із сервером
                client.Close(); //закриваємо повне зяднання

            }
            catch (Exception ex)
            {
                Console.WriteLine("Проблема при рооті із сервером {0}", ex.ToString());
            }
        }
    }
}