using Library_BookStore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_BookStore.Service
{
    public class BookSearchService
    {
        private readonly BookStoreContext _db;
        public BookSearchService(BookStoreContext db)
        {
            _db = db;
        }

        // SEARCH BY TITLE
        public List<Book> SearchByTitle(string title)
        {
            return _db.Books
                .Where(b => b.Title.Contains(title))
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Genre)
                .ToList();
        }

        // SEARCH BY AUTHOR
        public List<Book> SearchByAuthor(string authorName)
        {
            return _db.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Genre)
                .Where(b => b.Author.FullName.Contains(authorName))
                .ToList();
        }

        // SEARCH BY GENRE
        public List<Book> SearchByGenre(string genreName)
        {
            return _db.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Genre)
                .Where(b => b.Genre.Name.Contains(genreName))
                .ToList();
        }
    }
}
