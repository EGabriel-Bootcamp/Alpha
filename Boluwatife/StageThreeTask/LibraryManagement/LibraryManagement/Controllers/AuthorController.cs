using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthorController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Author>> Get()
        {
            return await _context.Authors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(m => m.AuthorId == id);
            if (author == null)
                return NotFound();
            return Ok(author);

        }

        [HttpPost]
        public async Task<IActionResult> Post(Author author)
        {
            _context.Add(author);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Author authorData)
        {
            if (authorData == null || authorData.AuthorId == 0)
                return BadRequest();

            var author = await _context.Authors.FindAsync(authorData.AuthorId);
            if (author == null)
                return NotFound();
            author.Name = authorData.Name;
            author.PublisherId = authorData.PublisherId;
            author.Publisher = authorData.Publisher;
            author.Book_Authors = authorData.Book_Authors;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null) return NotFound();
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
