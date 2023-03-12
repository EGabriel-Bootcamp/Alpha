namespace Library_Management_System.Models.Dtos
{
    /// <summary>
    /// Dto used for display of book when using any book endpoints
    /// </summary>
    public class BookForDisplayDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string ISBNNumber { get; set; }

        public string Version { get; set; }

        public List<AuthorForBookCreationDto> Authors { get; set; }
    }
}
