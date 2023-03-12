namespace Library_Management_System.Services
{
    public interface IBook
    {
        public string CreateBook(BookForCreationDto bookDto);
        public BookForDisplayDto GetABook(string bookName);
        public IEnumerable<BookForDisplayDto> GetAllBooks();
    }
}
