using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        
        public List<Book_Author> Book_Authors { get; set; }
    }
}
