using Library_Management_System.Data;
using Library_Management_System.Entities;
using System.Linq;

namespace Library_Management_System.Services
{
    /// <summary>
    /// Provides implementations for the IAuthor interface
    /// </summary>
    public class AuthorService : IAuthor
    {
        public readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string CreateAuthor(AuthorForCreationDto author) 
        {
            

            var publisherName = author.Publisher.PublisherName;
            var publisher = _context.Publishers.FirstOrDefault(x=>x.PublisherName.ToLower()== publisherName.ToLower());
            if (publisher != null)
            {
                var bookAuthor = new Author()
                {
                    AuthorDetails = author.AuthorDetails,
                    AuthorName = author.AuthorName,
                    Publisher = publisher,
                };

                _context.Authors.Add(bookAuthor);
                _context.SaveChanges();
                return "Author created successfully";
            }
            else
            {
                return "Publisher Not Found";
            }
        }

        public IEnumerable<AuthorForDisplayDto> GetAllAuthors()
        {

            var authorlist = _context.Authors.Select(author => author);
            if (authorlist.Count() <= 0)
            {
                return Enumerable.Empty<AuthorForDisplayDto>();
            }
            var authors = new List<AuthorForDisplayDto>();
            foreach (var author in authorlist)
            {
                var books_id = _context.Book_Authors.Where(a => a.AuthorId == author.AuthorId).Select(b => b.BookId);
                var books = new List<BookForAuthorDIsplayDto>();
                foreach (var book_id in books_id)
                {
                    var book = _context.Books.FirstOrDefault(b => b.BookId == book_id);
                    books.Add(new BookForAuthorDIsplayDto
                    {
                        Title = book.Title,
                        Description= book.Description,
                        ISBNNumber= book.ISBNNumber,
                        Version = book.Version
                    });
                }
                authors.Add(new AuthorForDisplayDto
                {
                    AuthorDetails = author.AuthorDetails,
                    AuthorName = author.AuthorName,
                    Books = books,
                });
            }
            return authors;
        }

        public IEnumerable<BookForAuthorDIsplayDto> GetAllBooksByAuthor(string authorName)
        {
            var author = _context.Authors.
                FirstOrDefault(x => x.AuthorName == authorName);
            
            if (author != null)
            {
                var books = new List<BookForAuthorDIsplayDto>();
                var books_id = _context.Book_Authors.Where(a => a.AuthorId == author.AuthorId).Select(b => b.BookId);
                foreach (var book_id in books_id)
                {
                    var book = _context.Books.FirstOrDefault(x => x.BookId == book_id);
                    books.Add(new BookForAuthorDIsplayDto
                    {
                        Title = book.Title,
                        Description = book.Description,
                        ISBNNumber = book.ISBNNumber,
                        Version = book.Version
                    });

                }
                return books;
            }
            return Enumerable.Empty<BookForAuthorDIsplayDto>();
        }

        public List<AuthorForDisplayDto> GetAuthor(string authorName)
        {
            var author = _context.Authors.
                FirstOrDefault(x => x.AuthorName == authorName);

            if (author != null)
            {
                var authorDto = new List<AuthorForDisplayDto>();

                var books_id = _context.Book_Authors.Where(a => a.AuthorId == author.AuthorId).Select(b => b.BookId);
                var books = new List<BookForAuthorDIsplayDto>();
                foreach (var book_id in books_id)
                {
                    var book = _context.Books.FirstOrDefault(b => b.BookId == book_id);
                    books.Add(new BookForAuthorDIsplayDto
                    {
                        Title = book.Title,
                        Description = book.Description,
                        ISBNNumber = book.ISBNNumber,
                        Version = book.Version
                    });
                }

                authorDto.Add(new AuthorForDisplayDto
                {
                    AuthorDetails = author.AuthorDetails,
                    AuthorName = author.AuthorName,
                    Books = books,
                });
                return authorDto;
            }
            return Enumerable.Empty<AuthorForDisplayDto>().ToList();
        }
    }
}
