using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_BookStore.Model
{
    public static class DbInitialize
    {
        public static void SeedGenres(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Novel" },
                new Genre { Id = 2, Name = "Fantasy" },
                new Genre { Id = 3, Name = "Poetry" },
                new Genre { Id = 4, Name = "Historical" },
                new Genre { Id = 5, Name = "Psychological" }
            );
        }

        public static void SeedAuthors(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, FullName = "Lina Kostenko" },
                new Author { Id = 2, FullName = "Serhiy Zhadan" },
                new Author { Id = 3, FullName = "Andriy Kurkov" },
                new Author { Id = 4, FullName = "Oksana Zabuzhko" },
                new Author { Id = 5, FullName = "Vasyl Shkliar" }
            );
        }

        public static void SeedPublishers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "Old Lion Publishing" },
                new Publisher { Id = 2, Name = "Folio" },
                new Publisher { Id = 3, Name = "Family Leisure Club" },
                new Publisher { Id = 4, Name = "Nora-Druk" }
            );
        }

        public static void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "ivan_user", Password = "12345" },
                new User { Id = 2, Username = "oleg_user", Password = "qwerty" },
                new User { Id = 3, Username = "admin", Password = "admin123" }
            );
        }

        public static void SeedCustomers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    FullName = "Ivan Rotko",
                    Phone = "0501234567",
                    UserId = 1
                },
                new Customer
                {
                    Id = 2,
                    FullName = "Oleg Koval",
                    Phone = "0679876543",
                    UserId = 2
                }
            );
        }

        public static void SeedBooks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Forest Song",
                    PageCount = 220,
                    YearPublished = 1956,
                    SalePrice = 180,
                    Quantity = 10,
                    AuthorId = 1,
                    PublisherId = 1,
                    GenreId = 3
                },
                new Book
                {
                    Id = 2,
                    Title = "Depeche Mode",
                    PageCount = 300,
                    YearPublished = 2004,
                    SalePrice = 250,
                    Quantity = 7,
                    AuthorId = 2,
                    PublisherId = 2,
                    GenreId = 1
                },
                new Book
                {
                    Id = 3,
                    Title = "Field Research of Ukrainian Sex",
                    PageCount = 280,
                    YearPublished = 1996,
                    SalePrice = 270,
                    Quantity = 5,
                    AuthorId = 4,
                    PublisherId = 3,
                    GenreId = 5
                },
                new Book
                {
                    Id = 4,
                    Title = "Messiah",
                    PageCount = 350,
                    YearPublished = 2010,
                    SalePrice = 300,
                    Quantity = 8,
                    AuthorId = 5,
                    PublisherId = 4,
                    GenreId = 4
                },
                new Book
                {
                    Id = 5,
                    Title = "Notes of a Ukrainian Madman",
                    PageCount = 320,
                    YearPublished = 2011,
                    SalePrice = 260,
                    Quantity = 6,
                    AuthorId = 3,
                    PublisherId = 1,
                    GenreId = 1
                }
            );
        }
    }
}
