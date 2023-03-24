using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("books")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository booksRepository;

        public BooksController(IBooksRepository repository)
        {
            this.booksRepository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBookAsync(CreateBookDTO bookDTO)
        {
            Book newBook = new() { Title = bookDTO.Title, AuthorId = bookDTO.AuthorId };
            return Ok(await booksRepository.CreateBookAsync(newBook));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBookByIdAsync(int id)
        {
            return Ok(await booksRepository.GetBookAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
        {
            return Ok(await booksRepository.GetBooksAsync());
        }
    }
}
