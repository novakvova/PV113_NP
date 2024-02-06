using MimeKit.Text;

namespace _4.SMTP_Email
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            SmtpEmailService emailService = new SmtpEmailService();
            Message info = new Message()
            {
                Subject = "Перевірка чи працює smtp",
                //Body = "Не втрачаємо час. Треба іти купатися",
                To = "bogdan.slonets@gmail.com"
            };
            string html = File.ReadAllText("html/index.html");
            //html = html.Replace("{$Title}", "Добре істи");
            info.Body = html;
            info.Subject = "Буде круто, якщо трапиться хороша дівчина!";
            emailService.Send(info);
            //emailService.DownloadMessages();
        }
    }
}