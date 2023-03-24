using Library.DTO;
using Library.Interfaces;
using Library.Models;
using Library.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [ApiController]
    [Route("publishers")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherRepository publisherRepository;

        public PublisherController(IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }

        [HttpPost]
        public async Task<ActionResult<PublisherDTO>> CreatePublisherAsync(
            CreatePublisherDTO publisherDTO
        )
        {
            Publisher pub = new() { Name = publisherDTO.Name, };

            return Ok(await publisherRepository.CreatePublisherAsync(pub));
        }

        [HttpGet]
        public async Task<IEnumerable<PublisherDTO>> GetPublishersAsync()
        {
            var publishers = (await publisherRepository.GetPublishersAsync()).Select(
                pub => pub.AsIs()
            );
            return publishers;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDTO>> GetPublisherAsync(int id)
        {
            var pub = await publisherRepository.GetPublisherAsync(id);
            if (pub is null)
            {
                return NotFound();
            }

            return pub.AsIs();
        }

        [HttpGet("{id}/authors")]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAttachedAuthorsByIdAsync(int id)
        {
            var authors = await publisherRepository.GetAuthorsByPublisherIdAsync(id);
            if (authors is null)
            {
                return NotFound();
            }
            var authorsDTO = authors.Select(author => author.AuthorList());
            return Ok(authorsDTO);
        }
    }
}
