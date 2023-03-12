namespace Library_Management_System.Services
{
    public interface IAuthor
    {
        public string CreateAuthor(AuthorForCreationDto author);
        public List<AuthorForDisplayDto> GetAuthor(string authorName);
        IEnumerable<AuthorForDisplayDto> GetAllAuthors();
        IEnumerable<BookForAuthorDIsplayDto> GetAllBooksByAuthor(string authorName);
    }
}
