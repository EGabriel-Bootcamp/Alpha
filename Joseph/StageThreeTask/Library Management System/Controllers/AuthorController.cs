using AutoMapper;
using Library_Management_System.Models.Dtos;
using Library_Management_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthor _author;
        private readonly IMapper _mapper;
        public AuthorController(IAuthor author, IMapper mapper)
        {
            _author = author;
            _mapper = mapper;
        }


        //Create
        [HttpPost("author/create")]
        public ActionResult Create([FromBody]AuthorForCreationDto author)
        {
            //Author bookAuthor = _mapper.Map<Author>(author);
            
            if (author == null) { return BadRequest(); }
            var status = _author.CreateAuthor(author);
            return Ok(status);
        }

        //GetAllAuthors
        [HttpGet("author/getAll")]
        public ActionResult GetAllAuthors()
        {
            var allAuthors = _author.GetAllAuthors();

            //return Ok(_mapper.Map<AuthorForDisplayDto>(allAuthors));
            List<AuthorForDisplayDto> allAuthorsDto = new List<AuthorForDisplayDto>();
            foreach (var authors in allAuthors)
            {
                allAuthorsDto.Add(new AuthorForDisplayDto()
                {
                    AuthorDetails = authors.AuthorDetails,
                    AuthorName  = authors.AuthorName,
                    //Publisher= authors.Publisher,
                    Books = authors.Books,
                });
            }
            return Ok(allAuthorsDto);
        }

        //GetAnAuthor
        [HttpGet("author/getAuthor")]
        public ActionResult GetAnAuthor(string authorName)
        {
            if (authorName == null) { return BadRequest(); }
            var authors = _author.GetAuthor(authorName);
            if (authors == null) 
            {
                return Ok("No author not found");
            }
            
            return Ok(authors);
        }

        //GetBooksAttachedToAnAuthor
        [HttpGet("author/getBooksByAuthor")]
        public ActionResult GetBooksAttachedToAnAuthor(string authorName)
        {
            if (authorName == null) { return BadRequest(); }
            var books = _author.GetAllBooksByAuthor(authorName);
            return Ok(books);
        }
    }
}
