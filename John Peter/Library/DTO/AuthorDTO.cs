using System.Runtime.Serialization;
using Library.Models;

public class AuthorDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public int? PublisherId { get; set; }

    public string? PublisherName { get; set; }

    public virtual ICollection<Book>? Books { get; set; }
}
