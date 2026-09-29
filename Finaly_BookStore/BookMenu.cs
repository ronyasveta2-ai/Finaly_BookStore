using Library_BookStore.Model;
using Library_BookStore.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finaly_BookStore
{

    public class BookMenu
    {
        private readonly BookStoreContext _db;
        private readonly BookService _bookService;

        public BookMenu(BookStoreContext db)
        {
            _db = db;
            _bookService = new BookService(db);
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;

                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║              BOOK MENU               ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║ 1. Додати книгу                      ║");
                Console.WriteLine("║ 2. Редагувати книгу                  ║");
                Console.WriteLine("║ 3. Видалити книгу                    ║");
                Console.WriteLine("║ 4. Продати книгу                     ║");
                Console.WriteLine("║ 5. Списати книгу                     ║");
                Console.WriteLine("║ 6. Показати всі книги                ║");
                Console.WriteLine("║ 0. Назад                             ║");
                Console.WriteLine("╚══════════════════════════════════════╝");

                Console.ResetColor();
                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        EditBook(); 
                        break;
                    case "3":
                        DeleteBook();
                        break;
                    case "4": 
                        SellBook(); 
                        break;
                    case "5":
                        WriteOffBook();
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

        private void AddBook()
        {
            Console.Clear();
            Console.WriteLine("Додавання книги");

            Book book = new Book();

            Console.Write("Назва: ");
            book.Title = Console.ReadLine();

            Console.Write("Кількість сторінок: ");
            book.PageCount = int.Parse(Console.ReadLine());

            Console.Write("Рік видання: ");
            book.YearPublished = int.Parse(Console.ReadLine());

            Console.Write("Ціна продажу: ");
            book.SalePrice = decimal.Parse(Console.ReadLine());

            Console.Write("Кількість: ");
            book.Quantity = int.Parse(Console.ReadLine());

            Console.Write("ID автора: ");
            book.AuthorId = int.Parse(Console.ReadLine());

            Console.Write("ID видавництва: ");
            book.PublisherId = int.Parse(Console.ReadLine());

            Console.Write("ID жанру: ");
            book.GenreId = int.Parse(Console.ReadLine());

            _bookService.AddBook(book);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Книгу додано!");
            Console.ResetColor();
            Console.ReadKey();
        }

        private void EditBook()
        {
            Console.Clear();
            Console.WriteLine("Редагування книги");

            Console.Write("ID книги: ");
            int id = int.Parse(Console.ReadLine());

            var book = _db.Books.Find(id);

            if (book == null)
            {
                Console.WriteLine("Книгу не знайдено!");
                Console.ReadKey();
                return;
            }

            Book updated = new Book();
            updated.Id = id;

            Console.Write($"Назва ({book.Title}): ");
            updated.Title = Console.ReadLine();

            Console.Write($"Сторінки ({book.PageCount}): ");
            updated.PageCount = int.Parse(Console.ReadLine());

            Console.Write($"Рік ({book.YearPublished}): ");
            updated.YearPublished = int.Parse(Console.ReadLine());

            Console.Write($"Ціна ({book.SalePrice}): ");
            updated.SalePrice = decimal.Parse(Console.ReadLine());

            Console.Write($"Кількість ({book.Quantity}): ");
            updated.Quantity = int.Parse(Console.ReadLine());

            Console.Write($"ID автора ({book.AuthorId}): ");
            updated.AuthorId = int.Parse(Console.ReadLine());

            Console.Write($"ID видавництва ({book.PublisherId}): ");
            updated.PublisherId = int.Parse(Console.ReadLine());

            Console.Write($"ID жанру ({book.GenreId}): ");
            updated.GenreId = int.Parse(Console.ReadLine());

            if (_bookService.EditBook(updated))
                Console.WriteLine("Книгу оновлено!");
            else
                Console.WriteLine("Помилка!");

            Console.ReadKey();
        }

        private void DeleteBook()
        {
            Console.Clear();
            Console.WriteLine("Видалення книги");

            Console.Write("ID книги: ");
            int id = int.Parse(Console.ReadLine());

            if (_bookService.DeleteBook(id))
                Console.WriteLine("Книгу видалено!");
            else
                Console.WriteLine("Помилка!");

            Console.ReadKey();
        }

        private void SellBook()
        {
            Console.Clear();
            Console.WriteLine("Продаж книги");

            Console.Write("ID книги: ");
            int bookId = int.Parse(Console.ReadLine());

            Console.Write("ID покупця: ");
            int customerId = int.Parse(Console.ReadLine());

            if (_bookService.SellBook(bookId, customerId))
                Console.WriteLine("Книга продана!");
            else
                Console.WriteLine("Помилка!");

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

            if (_bookService.WriteOffBook(bookId, reason))
                Console.WriteLine("Книгу списано!");
            else
                Console.WriteLine("Помилка!");

            Console.ReadKey();
        }
    }

    public class BookSearchMenu
    {
        private readonly BookStoreContext _db;
        private readonly BookSearchService _searchService;

        public BookSearchMenu(BookStoreContext db)
        {
            _db = db;
            _searchService = new BookSearchService(db);
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║           BOOK SEARCH MENU           ║");
                Console.WriteLine("╠══════════════════════════════════════╣");
                Console.WriteLine("║ 1. Пошук за назвою                   ║");
                Console.WriteLine("║ 2. Пошук за автором                  ║");
                Console.WriteLine("║ 3. Пошук за жанром                   ║");
                Console.WriteLine("║ 4. Показати всі книги                ║");
                Console.WriteLine("║ 0. Назад                             ║");
                Console.WriteLine("╚══════════════════════════════════════╝");

                Console.ResetColor();
                Console.Write("Ваш вибір: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SearchByTitle();
                        break;
                    case "2":
                        SearchByAuthor();
                        break;
                    case "3":
                        SearchByGenre(); 
                        break;
                    case "4":
                        var view = new BookViewService(_db);
                        view.ShowAllBooks();
                        break;
                    case "0": return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Невірний вибір!");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void SearchByTitle()
        {
            Console.Clear();
            Console.Write("Введіть назву: ");
            string title = Console.ReadLine();

            var books = _searchService.SearchByTitle(title);

            ShowBooks(books);
        }

        private void SearchByAuthor()
        {
            Console.Clear();
            Console.Write("Введіть автора: ");
            string author = Console.ReadLine();

            var books = _searchService.SearchByAuthor(author);

            ShowBooks(books);
        }

        private void SearchByGenre()
        {
            Console.Clear();
            Console.Write("Введіть жанр: ");
            string genre = Console.ReadLine();

            var books = _searchService.SearchByGenre(genre);

            ShowBooks(books);
        }

        private void ShowBooks(List<Book> books)
        {
            Console.Clear();

            if (books.Count == 0)
            {
                Console.WriteLine("Нічого не знайдено.");
                Console.ReadKey();
                return;
            }

            foreach (var b in books)
            {
                Console.WriteLine($"{b.Id} | {b.Title} | {b.Author.FullName} | {b.Genre.Name} | {b.SalePrice} грн | {b.Quantity} шт");
            }

            Console.ReadKey();
        }
    }
    public class BookViewService
    {
        private readonly BookStoreContext _db;

        public BookViewService(BookStoreContext db)
        {
            _db = db;
        }

        public void ShowAllBooks()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║             ВСІ КНИГИ                ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();

            var books = _db.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Publisher)
                .ToList();

            if (books.Count == 0)
            {
                Console.WriteLine("Немає книг.");
                Console.ReadKey();
                return;
            }

            foreach (var b in books)
            {
                Console.WriteLine($"{b.Id,-4} | {b.Title,-30} | {b.Author.FullName,-20} | {b.Genre.Name,-15} | {b.SalePrice} грн | {b.Quantity} шт");
            }

            Console.ReadKey();
        }
    }
}
