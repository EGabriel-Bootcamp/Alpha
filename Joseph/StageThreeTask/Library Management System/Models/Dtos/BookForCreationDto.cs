namespace Library_Management_System.Models.Dtos
{
    public class BookForCreationDto
    {
        public string Title { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        public string ISBNNumber { get; set; }

        public string Version { get; set; }

        public List<AuthorForBookCreationDto> Authors { get; set; }
        public string Publisher { get; set; }   
    }
}
