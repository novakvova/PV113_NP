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
                Subject = "На скільки зараз тепла вода",
                Body = "Не втрачаємо час. Треба іти купатися",
                To = "novakvova@gmail.com"
            };
            //emailService.Send(info);
            emailService.DownloadMessages();
        }
    }
}