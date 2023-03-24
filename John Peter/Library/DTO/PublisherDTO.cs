using Library.Models;

namespace Library.DTO
{
    public class PublisherDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Author> Authors { get; } = new List<Author>();
    }
}
