using Library.Models;

namespace Library.Interfaces
{
    public interface IAuthorsRepository
    {
        Task<Author> CreateAuthorAsync(Author author);
        Task<Author> GetAuthorByIdAsync(int id);
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<IEnumerable<Book>> GetBooksByAuthorIdAsync(int id);
    }
}
