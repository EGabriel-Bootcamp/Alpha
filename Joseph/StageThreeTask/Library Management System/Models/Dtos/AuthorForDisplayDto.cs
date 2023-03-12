namespace Library_Management_System.Models.Dtos
{
    public class AuthorForDisplayDto
    {
        public string AuthorName { get; set; }

        public string AuthorDetails { get; set; }

        public List<BookForAuthorDIsplayDto> Books { get; set; }
        //public Publisher Publisher { get; set; }

    }
}
