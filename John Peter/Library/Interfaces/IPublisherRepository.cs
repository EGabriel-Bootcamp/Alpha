using Library.DTO;
using Library.Models;

namespace Library.Interfaces
{
    public interface IPublisherRepository
    {
        Task<Publisher> CreatePublisherAsync(Publisher publisher);
        Task<IEnumerable<Author>> GetAuthorsByPublisherIdAsync(int publisherId);
        Task<Publisher> GetPublisherAsync(int id);
        Task<IEnumerable<Publisher>> GetPublishersAsync();
    }
}
