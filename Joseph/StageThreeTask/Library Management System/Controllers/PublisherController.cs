using AutoMapper;
using Library_Management_System.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    [Route("api")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisher _publisher;
        private readonly IMapper _mapper;
        public PublisherController(IPublisher publisher, IMapper mapper)
        {
            _publisher = publisher;
            _mapper = mapper;
        }
         
        /// <summary>
        /// Endpoint creates a new publisher
        /// </summary>
        /// <param name="publisherDto">An object representing a publisher</param>
        /// <returns>200(Success) or 400(BadRequest)</returns>
        [HttpPost("publisher/create")]
        public ActionResult Create(PublisherForCreation publisherDto)
        {

            if (publisherDto == null) { return BadRequest(); }            
            _publisher.CreatePublisher(publisherDto);
            return Ok("Publisher created successfully");
        }

        //Get A Publisher
        [HttpGet("publisher/getpublisher")]
        public ActionResult GetAPublisher(string publisherName)
        {
            if (publisherName == null) 
            {
                return BadRequest(); 
            }
            var publisher = _publisher.GetAPublisher(publisherName);
            return Ok(publisher);
        }

        //GetAllPublishers
        [HttpGet("publisher/getAllPublisher")]
        public ActionResult GetAllPublishers()
        {
            var publishers = _publisher.GetAllPublishers();
            return Ok(publishers);
        }

        //GetAuthorsAttachedToAPublisher
        [HttpGet("publisher/getAuthorsByPublishers")]
        public ActionResult GetAuthorsAttachedToAPublisher(string publisherName)
        {
            if (publisherName == null) 
            { 
                return BadRequest(); 
            }
            var authors = _publisher.GetAuthorsAttachedToAPublisher(publisherName);
            return Ok(authors);
        }
    }

}
