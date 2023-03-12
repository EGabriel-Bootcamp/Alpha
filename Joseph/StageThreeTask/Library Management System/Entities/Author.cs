using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Entities
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        public string AuthorName { get; set; }

        [Required]
        [MaxLength(100)]
        public string AuthorDetails { get; set; }
        public List<Book_Author> Book_Authors { get; set; }
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }


    }
}
