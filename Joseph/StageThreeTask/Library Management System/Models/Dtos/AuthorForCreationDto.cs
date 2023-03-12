namespace Library_Management_System.Models.Dtos
{
    /// <summary>
    /// Dto used for author creation
    /// </summary>
    public class AuthorForCreationDto
    {
        [Required]
        public string AuthorName { get; set; }

        [Required]
        [MaxLength(100)]
        public string AuthorDetails { get; set; }

        public  PublisherForCreation Publisher { get; set; }

    }
}
