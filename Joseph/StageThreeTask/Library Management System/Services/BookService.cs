using Library_Management_System.Data;
using Library_Management_System.Entities;

namespace Library_Management_System.Services
{
    /// <summary>
    /// Provides implementations for the BookAuthor class
    /// </summary>
    public class BookService : IBook
    {
        public readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string CreateBook(BookForCreationDto bookDto)
        {
            string operationStatus;
            var authors = new List<Author>();
            var authorExist = false;

            var authorName = bookDto.Authors;
            // Check if author exist
            foreach (var author in authorName)
            {

                var auth = _context.Authors
                                        .Where(x => x.AuthorName == author.AuthorName)
                                        .Select(x => x);
                if (auth.Count() <= 0)
                {
                    authorExist = false;
                    break;
                }
                else
                {
                    var a = _context.Authors.FirstOrDefault(x => x.AuthorName.ToLower() == author.AuthorName.ToLower());
                    authors.Add(a);
                    authorExist = true;
                }
            };

            //checked if publisher exist
            if (!authorExist)
            {
                operationStatus = "One or more authors not found in the database";
                return operationStatus;
            }

            var pub = _context.Publishers.FirstOrDefault(x => x.PublisherName == bookDto.Publisher);
            if (pub == null)
            {
                operationStatus = "No publisher found with the supplied name";
                return operationStatus;
            }
            else
            {
                var author = _context.Authors.Select(x=> x.AuthorName).FirstOrDefault();
                var book = new Book
                {
                    Description = bookDto.Description,
                    ISBNNumber = bookDto.ISBNNumber,
                    Title = bookDto.Title,
                    Version = bookDto.Version,
                };                

                _context.Books.Add(book);
                _context.SaveChanges();

                foreach (var authorN in authors)
                {
                    var book_author = new Book_Author
                    {
                        AuthorId = authorN.AuthorId,
                        BookId = book.BookId,
                    };
                    _context.Book_Authors.Add(book_author);
                    _context.SaveChanges();
                }

                operationStatus = "Book creation successfull";
                return operationStatus;

            }
        }


        public BookForDisplayDto GetABook(string bookName)
        {
            var book = _context.Books.FirstOrDefault(x => x.Title.Contains(bookName));

            if (book != null)
            {
                var book_author = _context.Book_Authors.Where(x => x.BookId == book.BookId).Select(x => x.AuthorId);
                var authorList = new List<AuthorForBookCreationDto>();
                foreach (var author_id in book_author)
                {
                    var author = _context.Authors.FirstOrDefault(x => x.AuthorId == author_id);
                    authorList.Add(new AuthorForBookCreationDto
                    {
                        AuthorName = author.AuthorName
                    });
                }

                var bookDto = new BookForDisplayDto
                {
                    Title = book.Title,
                    Authors = authorList,
                    Description = book.Description,
                    ISBNNumber = book.ISBNNumber,
                    Version = book.Version
                };

                return bookDto;
            }
            return new BookForDisplayDto();

        }

        public IEnumerable<BookForDisplayDto> GetAllBooks()
        {
            var books = _context.Books.Select(x=>x);
            var bookDto = new List<BookForDisplayDto>();
            var authorList = new List<AuthorForBookCreationDto>();

            foreach (var book in books)
            {
                var book_author = _context.Book_Authors.Where(x => x.BookId == book.BookId).Select(x => x.AuthorId);
                foreach (var author_id in book_author)
                {
                    authorList.Add(new AuthorForBookCreationDto
                    {
                        AuthorName = _context.Authors.FirstOrDefault(a=>a.AuthorId == author_id).AuthorName,  
                    });
                }

                bookDto.Add(new BookForDisplayDto
                {
                    ISBNNumber = book.ISBNNumber,
                    Authors = authorList,
                    Description = book.Description,
                    Title = book.Title,
                    Version = book.Version
                });
            }
            return bookDto;
        }
    }
}
