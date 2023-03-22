using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public List<Author> Authors { get; set; }
    }
}
