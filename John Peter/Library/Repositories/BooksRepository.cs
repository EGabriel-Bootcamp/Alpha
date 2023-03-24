using Library.Context;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    class BooksRepository : IBooksRepository
    {
        private readonly LibraryContext dbContext;

        public BooksRepository(LibraryContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await dbContext.Books.Include(b => b.Authors).ToListAsync();
        }

        public async Task<Book> GetBookAsync(int bookId)
        {
            var books = await dbContext.Books.FindAsync(bookId);
            return books;
        }
    }
}
