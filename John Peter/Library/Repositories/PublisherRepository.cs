using Library.Context;
using Library.DTO;
using Library.Interfaces;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    class PublisherRepository : IPublisherRepository
    {
        private readonly LibraryContext dbContext;

        public PublisherRepository(LibraryContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Publisher> CreatePublisherAsync(Publisher publisher)
        {
            await dbContext.Publishers.AddAsync(publisher);
            await dbContext.SaveChangesAsync();
            return publisher;
        }

        public async Task<Publisher> GetPublisherAsync(int id)
        {
            return await dbContext.Publishers.FindAsync(id);
        }

        public async Task<IEnumerable<Publisher>> GetPublishersAsync()
        {
            return await dbContext.Publishers.ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsByPublisherIdAsync(int publisherId)
        {
            var publisher = await dbContext.Publishers
                .Include(p => p.Authors)
                .ThenInclude(r => r.Books)
                .FirstOrDefaultAsync(p => p.Id == publisherId);

            return publisher.Authors;
        }
    }
}
