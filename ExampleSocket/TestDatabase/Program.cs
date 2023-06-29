using TestDatabase.Data;
using TestDatabase.Data.Entities;

namespace TestDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            int action = 0;
            var context = new AppEFContext();
            do
            {
                Console.WriteLine("0. Вихід");
                Console.WriteLine("1. Додати користувача");
                Console.WriteLine("2. Показати усіх користувачів");
                Console.Write("->_");
                action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        {
                            var user = new UserEntity();
                            Console.Write("Вкажіть ім'я користувача: ");
                            user.Name= Console.ReadLine();
                            Console.Write("Вкажіть пошту користувача: ");
                            user.Email = Console.ReadLine();
                            Console.Write("Вкажіть пароль користувача: ");
                            user.Password = Console.ReadLine();
                            context.Users.Add(user);
                            context.SaveChanges();
                            break;
                        }
                    case 2:
                        {
                            foreach(var user in context.Users)
                            {
                                Console.WriteLine("\n----------------------------------");
                                Console.WriteLine("Id: {0}",user.Id);
                                Console.WriteLine("Ім'я: {0}",user.Name);
                                Console.WriteLine("Пошта: {0}",user.Email);
                                Console.WriteLine("Пароль: {0}",user.Password);
                            }
                            break;
                        }
                }

            } while (action != 0);
        }
    }
}