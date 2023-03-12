using AutoMapper;
using Library_Management_System.Models.Dtos;
using Library_Management_System.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library_Management_System.Controllers
{
    [Route("api")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBook _book;
        private readonly IMapper _mapper;
        public BookController(IBook book, IMapper mapper)
        {
            _book = book;
            _mapper = mapper;
        }

        // Create
        [HttpPost("create-book")]
        public ActionResult Create(BookForCreationDto bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest();
            }
            var book = _book.CreateBook(bookDto);
            return Ok(book);
        }

        // GetABook
        [HttpGet("get-book")]
        public ActionResult GetABook(string bookName)
        {
            if (bookName == null)
            {
                return BadRequest();
            }
            var book = _book.GetABook(bookName);
            return Ok(book);
        }

        // GetAllBooks
        [HttpGet("books")]
        public ActionResult GetAllBooks()
        {
            var books = _book.GetAllBooks();
            return Ok(books);
        }
    }
}
