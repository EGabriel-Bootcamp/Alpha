using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Author>()
                .HasOne(book => book.Book)
                .WithMany(book_author => book_author.Book_Authors)
                .HasForeignKey(book_id => book_id.BookId);

            modelBuilder.Entity<Book_Author>()
                .HasOne(author => author.Author)
                .WithMany(book_author => book_author.Book_Authors)
                .HasForeignKey(author_id => author_id.AuthorId);
        }

        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Book_Author> Book_Authors { get; set; }
    }
}
