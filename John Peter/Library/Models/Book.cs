using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int AuthorId { get; set; }

    // public Author? Author { get; set; }

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
