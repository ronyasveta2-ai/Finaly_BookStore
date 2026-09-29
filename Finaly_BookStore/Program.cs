using Finaly_BookStore;
using Library_BookStore.Model;
using System.Text;


class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.WriteLine("Hello, World!");
        
        //using var db = new BookStoreContext();
        //var books = db.Books.ToList();
        //foreach (var book in books)
        //    Console.WriteLine(book.Title);

        var app = new BookStoreApp();
        app.Start();
    }
}