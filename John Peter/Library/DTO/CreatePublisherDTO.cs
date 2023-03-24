using System.ComponentModel.DataAnnotations;
using Library.Models;

namespace Library.DTO
{
    public class CreatePublisherDTO
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Author> Authors { get; } = new List<Author>();
    }
}
