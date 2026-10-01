using System;
using System.Collections.Generic;

namespace EFCoreSetupApp.Data;

public partial class Author
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
