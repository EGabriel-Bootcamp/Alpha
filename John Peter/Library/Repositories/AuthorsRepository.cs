using Library.Context;
using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    class AuthorsRepository : IAuthorsRepository
    {
        private readonly LibraryContext dbContext;

        public AuthorsRepository(LibraryContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Author> CreateAuthorAsync(Author author)
        {
            await dbContext.Authors.AddAsync(author);
            await dbContext.SaveChangesAsync();

            return author;
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await dbContext.Authors.Include(author => author.Publisher).ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await dbContext.Authors.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int id)
        {
            var author = await dbContext.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            return author.Books;
        }
    }
}
