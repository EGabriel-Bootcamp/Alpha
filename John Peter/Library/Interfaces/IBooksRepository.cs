using Library.Models;

public interface IBooksRepository
{
    Task<Book> CreateBookAsync(Book book);
    Task<Book> GetBookAsync(int bookId);
    Task<IEnumerable<Book>> GetBooksAsync();
}
