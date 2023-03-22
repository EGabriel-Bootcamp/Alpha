using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Book_Author> Book_Authors { get; set; }

        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }


    }
}
