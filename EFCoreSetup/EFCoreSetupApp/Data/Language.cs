using System;
using System.Collections.Generic;

namespace EFCoreSetupApp.Data;

public partial class Language
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
