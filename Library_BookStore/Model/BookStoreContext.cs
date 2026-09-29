using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;

namespace Library_BookStore.Model
{
    public class BookStoreContext : DbContext
    {
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;
                  Database=1_BookStoreDb;
                  Trusted_Connection=True;");
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<WriteOff> WriteOffs { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);
           
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(u => u.Customer)
                .HasForeignKey<Customer>(c => c.UserId);

            DbInitialize.SeedGenres(modelBuilder);
            DbInitialize.SeedAuthors(modelBuilder);
            DbInitialize.SeedPublishers(modelBuilder);
            DbInitialize.SeedUsers(modelBuilder);
            DbInitialize.SeedCustomers(modelBuilder);
            DbInitialize.SeedBooks(modelBuilder);

        }
    }
    public class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }

    }
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public int PageCount { get; set; }

        [Required]
        public int YearPublished { get; set; }

        [Required]
        public decimal SalePrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        // Fk - Genre
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

    }

    public class Author
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string FullName { get; set; }

        public ICollection<Book> Books { get; set; }
    }

    public class Publisher
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; } 
    }

    public class Customer
    {
        public Customer()
        {
            Sales = new HashSet<Sale>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(120)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Sale> Sales { get; set; } 
    }

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        public Customer Customer { get; set; }
    }

    public class Sale
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public decimal Price { get; set; }
    }

    public class WriteOff
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(200)]
        public string Reason { get; set; }
    }
}
