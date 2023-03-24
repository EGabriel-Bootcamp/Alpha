using Library.DTO;
using Library.Interfaces;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorsRepository authorsRepository;

        public AuthorController(IAuthorsRepository authorsRepository)
        {
            this.authorsRepository = authorsRepository;
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> CreateAuthorAsync(CreateAuthorDTO authorDTO)
        {
            Author newAuthor = new() { Name = authorDTO.Name, PublisherId = authorDTO.PublisherId };
            return Ok(await authorsRepository.CreateAuthorAsync(newAuthor));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthorsAsync()
        {
            var authors = await authorsRepository.GetAuthorsAsync();
            return Ok(authors.Select(aut => aut.AuthorList()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthorByIdAsync(int id)
        {
            var author = await authorsRepository.GetAuthorByIdAsync(id);
            if (author is null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpGet("{id}/books")]
        public async Task<ActionResult<IEnumerable<Book>>> GetAuthorBooksByIdAsync(int id)
        {
            var books = await authorsRepository.GetBooksByAuthorIdAsync(id);
            return Ok(books.Select(book => book.BookList()));
        }
    }
}
