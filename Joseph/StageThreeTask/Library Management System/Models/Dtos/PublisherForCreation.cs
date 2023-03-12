namespace Library_Management_System.Models.Dtos
{
    /// <summary>
    /// Dto used when creating a publisher
    /// </summary>
    public class PublisherForCreation
    {

        [Required]
        public string PublisherName { get; set; }

        [Required]
        [MaxLength(250)]
        public string CopyRightLicense { get; set; }

        [Required]
        public string PublisherAddress { get; set; }
    }
}
