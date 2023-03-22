using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PublisherController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Publisher>> Get()
        {
            return await _context.Publishers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(m => m.PublisherId == id);
            if (publisher == null)
                return NotFound();
            return Ok(publisher);

        }

        [HttpPost]
        public async Task<IActionResult> Post(Publisher publisher)
        {
            _context.Add(publisher);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Publisher publisherData)
        {
            if (publisherData == null || publisherData.PublisherId == 0)
                return BadRequest();

            var publisher = await _context.Publishers.FindAsync(publisherData.PublisherId);
            if (publisher == null)
                return NotFound();
            publisher.Name = publisherData.Name;
            publisher.Address = publisherData.Address;
            publisher.Authors = publisherData.Authors;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null) return NotFound();
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
