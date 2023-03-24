using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Publisher
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; } = new List<Author>();
}
