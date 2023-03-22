using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
                return NotFound();
            return Ok(book);

        }

        [HttpPost]
        public async Task<IActionResult> Post(Book book)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Book bookData)
        {
            if (bookData == null || bookData.BookId == 0)
                return BadRequest();

            var book = await _context.Books.FindAsync(bookData.BookId);
            if (book == null)
                return NotFound();
            book.Title = bookData.Title;
            book.Description = bookData.Description;
            book.Book_Authors = bookData.Book_Authors;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
