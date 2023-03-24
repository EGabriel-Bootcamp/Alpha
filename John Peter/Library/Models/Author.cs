using System;
using System.Collections.Generic;

namespace Library.Models;

public class Author
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? PublisherId { get; set; }

    public Publisher? Publisher { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
