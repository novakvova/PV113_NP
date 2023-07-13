using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace _3.WPFChatClient
{
    public class ChatMessage
    {
        //Id - користувача - видається через код
        public string UserId { get; set; }
        //Ім'я користувача - Вова, Саша, ...
        public string Name { get; set; }
        //Текст повідомлення
        public string Text { get; set; }
        //Фото користувача
        public string Photo { get; set; }

        public byte[] Serialize()
        {
            using(var m = new MemoryStream())
            {
                using(BinaryWriter bw = new BinaryWriter(m))
                {
                    bw.Write(UserId);
                    bw.Write(Name);
                    bw.Write(Text);
                    bw.Write(Photo);
                }
                return m.ToArray();
            }
        }
        public static ChatMessage Desserialize(byte[] data)
        {
            ChatMessage msg = new ChatMessage();

            using (var m = new MemoryStream(data))
            {
                using(BinaryReader br = new BinaryReader(m))
                {
                    msg.UserId = br.ReadString();
                    msg.Name = br.ReadString();
                    msg.Text = br.ReadString();
                    msg.Photo = br.ReadString();
                }
            }
            return msg;
        }

        public static BitmapImage ToBitmapImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;//CacheOption must be set after BeginInit()
                img.StreamSource = ms;
                img.EndInit();

                if (img.CanFreeze)
                {
                    img.Freeze();
                }
                return img;
            }
        }
    }
}
