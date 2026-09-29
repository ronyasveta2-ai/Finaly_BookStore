using Library_BookStore.Model;
using Library_BookStore.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finaly_BookStore
{
    public class LoginMenu
    {
        private readonly UserService _userService;

        public LoginMenu(UserService userService)
        {
            _userService = userService;
        }

        public User Start()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║              LOGIN MENU              ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║  Введіть логін та пароль             ║");
                Console.WriteLine("║  0. Вихід                            ║");
                Console.WriteLine("╚══════════════════════════════════════╝");

                Console.ResetColor();

                Console.Write("Логін: ");
                string username = Console.ReadLine();

                if (username == "0")
                    return null;

                Console.Write("Пароль: ");
                string password = Console.ReadLine();

                var user = _userService.Login(username, password);

                if (user == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невірний логін або пароль!");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Успішний вхід!");
                Console.ResetColor();
                Console.ReadKey();

                return user;
            }
        }
    }
}
