namespace Library_Management_System.Entities
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }

        [Required]
        public string PublisherName { get; set; }

        [Required]
        [MaxLength(250)]
        public string CopyRightLicense { get; set; }

        [Required]
        public string PublisherAddress { get; set; }
        
 
        public List<Author> Authors{ get; set; } = new List<Author>();
    }
}
