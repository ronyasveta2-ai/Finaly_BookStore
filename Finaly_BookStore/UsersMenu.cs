using Library_BookStore.Model;
using Library_BookStore.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finaly_BookStore
{
    public class MainMenuAdmin
    {
        private readonly BookStoreContext _db;

        public MainMenuAdmin(BookStoreContext db)
        {
            _db = db;
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║             ADMIN MENU               ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║ 1. Керування книгами (CRUD)          ║");
                Console.WriteLine("║ 2. Пошук книг                        ║");
                Console.WriteLine("║ 3. Продати книгу                     ║");
                Console.WriteLine("║ 4. Списати книгу                     ║");
                Console.WriteLine("║ 5. Перегляд покупців                 ║");
                Console.WriteLine("║ 6. Показати всі книги                ║");
                Console.WriteLine("║ 0. Вихід                             ║");
                Console.WriteLine("╚══════════════════════════════════════╝");

                Console.ResetColor();
                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var bookMenu = new BookMenu(_db);
                        bookMenu.Start();
                        break;

                    case "2":
                        var searchMenu = new BookSearchMenu(_db);
                        searchMenu.Start();
                        break;

                    case "3":
                        // Продаж книги
                        SellBook();
                        break;

                    case "4":
                        // Списання книги
                        WriteOffBook();
                        break;

                    case "5":
                        ShowCustomers();
                        break;
                    case "6":
                        var view = new BookViewService(_db);
                        view.ShowAllBooks();
                        break;

                    case "0":
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Невірний вибір!");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void SellBook()
        {
            Console.Clear();
            Console.WriteLine("Продаж книги");

            Console.Write("ID книги: ");
            int bookId = int.Parse(Console.ReadLine());

            Console.Write("ID покупця: ");
            int customerId = int.Parse(Console.ReadLine());

            var service = new BookService(_db);

            if (service.SellBook(bookId, customerId))
                Console.WriteLine("Книга успішно продана!");
            else
                Console.WriteLine("Помилка продажу!");

            Console.ReadKey();
        }

        private void WriteOffBook()
        {
            Console.Clear();
            Console.WriteLine("Списання книги");

            Console.Write("ID книги: ");
            int bookId = int.Parse(Console.ReadLine());

            Console.Write("Причина: ");
            string reason = Console.ReadLine();

            var service = new BookService(_db);

            if (service.WriteOffBook(bookId, reason))
                Console.WriteLine("Книга списана!");
            else
                Console.WriteLine("Помилка списання!");

            Console.ReadKey();
        }

        private void ShowCustomers()
        {
            Console.Clear();
            Console.WriteLine("Список покупців:");

            foreach (var c in _db.Customers)
            {
                Console.WriteLine($"{c.Id} | {c.FullName} | {c.Phone}");
            }

            Console.ReadKey();
        }
    }

    public class MainMenuCustomer
    {
        private readonly BookStoreContext _db;
        private readonly User _user;

        public MainMenuCustomer(BookStoreContext db, User user)
        {
            _db = db;
            _user = user;
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;

                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║            CUSTOMER MENU             ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║ 1. Пошук книг                        ║");
                Console.WriteLine("║ 2. Мої покупки                       ║");
                Console.WriteLine("║ 3. Купити книгу                      ║");
                Console.WriteLine("║ 4. Показати всі книги                ║");
                Console.WriteLine("║ 0. Вихід                             ║");
                Console.WriteLine("╚══════════════════════════════════════╝");

                Console.ResetColor();
                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var searchMenu = new BookSearchMenu(_db);
                        searchMenu.Start();
                        break;

                    case "2":
                        ShowMyPurchases();
                        break;

                    case "3":
                        BuyBook();
                        break;
                    case "4":
                        var view = new BookViewService(_db);
                        view.ShowAllBooks();
                        break;

                    case "0":
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Невірний вибір!");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ShowMyPurchases()
        {
            Console.Clear();
            Console.WriteLine("Мої покупки:");

            var customer = _db.Customers.FirstOrDefault(c => c.UserId == _user.Id);

            if (customer == null)
            {
                Console.WriteLine("Помилка: покупця не знайдено.");
                Console.ReadKey();
                return;
            }

            var sales = _db.Sales.Where(s => s.CustomerId == customer.Id).ToList();

            foreach (var s in sales)
            {
                Console.WriteLine($"{s.Id} | {s.Book.Title} | {s.Price} грн | {s.Date}");
            }

            Console.ReadKey();
        }

        private void BuyBook()
        {
            Console.Clear();
            Console.WriteLine("Купівля книги");

            Console.Write("ID книги: ");
            int bookId = int.Parse(Console.ReadLine());

            var customer = _db.Customers.FirstOrDefault(c => c.UserId == _user.Id);

            if (customer == null)
            {
                Console.WriteLine("Помилка: покупця не знайдено.");
                Console.ReadKey();
                return;
            }

            var service = new BookService(_db);

            if (service.SellBook(bookId, customer.Id))
                Console.WriteLine("Книга успішно куплена!");
            else
                Console.WriteLine("Помилка купівлі!");

            Console.ReadKey();
        }
    }
}
