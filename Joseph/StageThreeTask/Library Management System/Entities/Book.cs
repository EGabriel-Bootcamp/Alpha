using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public string Title { get; set; }

        [MaxLength(150)]
        public string Description { get; set; }

        [Required]
        public string ISBNNumber { get; set; }

        public string Version { get; set; }

        public List<Book_Author> Book_Authors { get; set; }

    }
}
