using _3.WPFChatClient.dto;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3.WPFChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //фото користувача
        string image;
        TcpClient client = new TcpClient(); //клієнт, який іде при підключені до сервера
        NetworkStream ns;
        Thread thread;
        //Повідомлення, яке відправляємо на сервер
        ChatMessage _message = new ChatMessage();

        public MainWindow()
        {
            InitializeComponent();
        }
        //відключення від сервера
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _message.Text = "Покинув чат"; //Повідомленяємо усім, що ми покинули чат
            var buffer = _message.Serialize();
            ns.Write(buffer);

            client.Client.Shutdown(SocketShutdown.Send);
            client.Close();
        }

        //вибір фото
        private void btnPhotoSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            string filePath = dlg.FileName;
            var bytes = File.ReadAllBytes(filePath);
            var base64 = Convert.ToBase64String(bytes);
            UploadDTO upload = new UploadDTO
            {
                Photo = base64
            };
            string json = JsonConvert.SerializeObject(upload);
            bytes = Encoding.UTF8.GetBytes(json);
            string serverUrl = "https://pv113.itstep.click";
            WebRequest request = WebRequest.Create($"{serverUrl}/api/gallery/upload");
            request.Method = "POST";
            request.ContentType = "application/json";
            using (var stream = request.GetRequestStream())
            {
                stream.Write(bytes, 0, bytes.Length);
            }
            try
            {
                var response = request.GetResponse();
                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    string data = stream.ReadToEnd();
                    var resp = JsonConvert.DeserializeObject<UploadResponseDTO>(data);
                    image = resp.Image;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        //підлкючення до сервера
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                int port = 1023;
                _message.UserId = Guid.NewGuid().ToString();
                _message.Name = txtUserName.Text;
                _message.Photo = image;
                client.Connect(ip, port);
                ns = client.GetStream(); //получаю вказівник на потік
                thread = new Thread(o => ReceivedData((TcpClient)o));
                thread.Start(client);
                bntSend.IsEnabled = true;
                btnConnect.IsEnabled = false;
                txtUserName.IsEnabled = false;
                _message.Text = "Приєнався до чату";
                var buffer = _message.Serialize();
                ns.Write(buffer);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Problem connect "+ex.Message);
            }
            
        }

        private void ReceivedData(TcpClient client) //отримує дані від сервера через даний метод
        {
            NetworkStream ns = client.GetStream();
            byte[] readBytes = new byte[16054400];
            int byte_count;
            while((byte_count = ns.Read(readBytes))>0) {

            }
        }
        //надсилаємо повідомлення на сервер
        private void bntSend_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
