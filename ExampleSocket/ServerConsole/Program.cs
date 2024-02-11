using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IPAddress ipAddress;
        int portTest= 9076;
        string hostName = Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(hostName);
            ipAddress = null;
            for (int i = 0; i < ipHostInfo.AddressList.Length; ++i)
            {
                Console.WriteLine("Addres {0}", ipHostInfo.AddressList[i]);
                if (ipHostInfo.AddressList[i].AddressFamily ==
                  AddressFamily.InterNetwork)
                {
                    ipAddress = ipHostInfo.AddressList[i];
                    break;
                }
            }
            /*
                //IP - адреса нашого ПК
            IPAddress ip = IPAddress.Parse("127.0.0.1");
           // IPAddress ip = IPAddress.Parse("91.238.103.135");
            int port = 9076; //порт на якому працює наш сервер
            //ідертифікатор нашого сервера
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            //забезпучує роботу на основі протоколу Tcp
            Socket server = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Bind(endPoint); //сокет буде слухати запити по даноій адресі і порту
                server.Listen(10); //починаємо прослуховування запитів
                Console.WriteLine("Сервер запущено {0}", endPoint);
                while (true)
                {
                    //очікуємо запит від клієнта
                    Socket client = server.Accept(); //якийсь клієнт прислав нам запит
                    int bytes = 0; //кількість байт
                    byte[] data = new byte[1024]; //масив збірігає повідомлення клієнта
                    //Читаємо повідомлення клієнта
                    do
                    {
                        bytes = client.Receive(data); //читаємо байти від клієнта
                        Console.WriteLine("Повідомлення {0}", Encoding.Unicode.GetString(data));
                    } while (client.Available > 0); //якщо кількість байт, які ми читаємо більші 0

                    Console.WriteLine("Client EndPoint {0}", client.RemoteEndPoint);
                    string message = "Дякую. Сервер отримав ваше повідомлення.";
                    data = Encoding.Unicode.GetBytes(message); //перетворюємо повідомлення в байти
                    client.Send(data); //сервер відправляє клієнту інформацію
                    client.Shutdown(SocketShutdown.Both); //завершуємо роботу із клієнтом
                    client.Close(); //повністю видаляємо зяднання
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Помилка роботи програми {0}", ex.Message);
            }
            */
        }
    }
}