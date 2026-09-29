using Library_BookStore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_BookStore.Service
{
    public class BookService
    {
        private readonly BookStoreContext _db;

        public BookService(BookStoreContext db)
        {
            _db = db;
        }

        // ADD BOOK
        public void AddBook(Book book)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
        }

        // DELETE BOOK
        public bool DeleteBook(int id)
        {
            var book = _db.Books.Find(id);
            if (book == null)
                return false;

            _db.Books.Remove(book);
            _db.SaveChanges();
            return true;
        }

        // EDIT BOOK 
        public bool EditBook(Book updatedBook)
        {
            var book = _db.Books.Find(updatedBook.Id);
            if (book == null)
                return false;

            book.Title = updatedBook.Title;
            book.PageCount = updatedBook.PageCount;
            book.YearPublished = updatedBook.YearPublished;
            book.SalePrice = updatedBook.SalePrice;
            book.Quantity = updatedBook.Quantity;
            book.AuthorId = updatedBook.AuthorId;
            book.PublisherId = updatedBook.PublisherId;
            book.GenreId = updatedBook.GenreId;

            _db.SaveChanges();
            return true;
        }

        // SELL BOOK
        public bool SellBook(int bookId, int customerId)
        {
            var book = _db.Books.Find(bookId);
            var customer = _db.Customers.Find(customerId);

            if (book == null || customer == null)
                return false;

            if (book.Quantity <= 0)
                return false;

            // Create sale record
            var sale = new Sale
            {
                BookId = bookId,
                CustomerId = customerId,
                Date = DateTime.Now,
                Price = book.SalePrice
            };

            _db.Sales.Add(sale);

            book.Quantity--;

            _db.SaveChanges();
            return true;
        }

        // WRITE OFF BOOK
        public bool WriteOffBook(int bookId, string reason)
        {
            var book = _db.Books.Find(bookId);
            if (book == null)
                return false;

            if (book.Quantity <= 0)
                return false;

            var writeOff = new WriteOff
            {
                BookId = bookId,
                Date = DateTime.Now,
                Reason = reason
            };

            _db.WriteOffs.Add(writeOff);

            book.Quantity--;

            _db.SaveChanges();
            return true;
        }
    }
}
