using Library.Models;

public class BookDTO
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
    public virtual ICollection<Author> Authors { get; set; }
}
