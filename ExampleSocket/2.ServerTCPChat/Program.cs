using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _2.ServerTCPChat
{
    internal class Program
    {
        //Блокування роботи потоку, щоб інші користувачі не міняли даних. Для безпечної роботи
        static readonly object _lock = new object();
        //Набір клієнтів, які працюють в чаті - війшли в чат
        static readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            int count = 1; //лічильник клієнтів чату
            string fileName = "config.txt";
            IPAddress ip;
            int port;
            using(FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using(StreamReader sr = new StreamReader(fs))
                {
                    ip = IPAddress.Parse(sr.ReadLine());
                    port = int.Parse(sr.ReadLine());
                }
            }
            TcpListener socketServer = new TcpListener(ip,port);
            socketServer.Start();
            Console.WriteLine("Запуск сервера {0}:{1}", ip, port);
            while(true)
            {
                TcpClient client = socketServer.AcceptTcpClient();
                lock(_lock)
                {
                    list_clients.Add(count, client);
                }
                Console.WriteLine("Появився на сервері новий клієнт {0}", client.Client.RemoteEndPoint);
                Thread t = new Thread(handle_clients);
                t.Start(count);
                lock(_lock)
                {
                    count++;
                }
            }

        }
        public static void handle_clients(object c)
        {
            int id = (int)c;
            TcpClient client;
            lock (_lock)
            {
                client = list_clients[id];
            }
            while(true)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[400096];
                int byte_count = stream.Read(buffer);
                if (byte_count == 0)
                    break; //клієнт вийшов із чату
                string data = Encoding.UTF8.GetString(buffer);
                Console.WriteLine("Client Message {0}", data);
                broadcast(data); //розсилаємо повідомлення усім клієнтам чату
            }
            lock(_lock)
            {
                Console.WriteLine("Клієнт покинув чат {0}", client.Client.RemoteEndPoint);
                list_clients.Remove(id); //видаляємо клієнта із чату, щоб йому не відпарвлять повідомлення
            }
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }

        public static void broadcast(string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            lock(_lock)
            {
                foreach(var c in list_clients.Values)
                {
                    NetworkStream stream = c.GetStream(); //потік клієнта
                    stream.Write(buffer); //відправляю повідомлення
                }
            }
        }
    }
}