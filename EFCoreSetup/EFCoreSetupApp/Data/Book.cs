using System;
using System.Collections.Generic;

namespace EFCoreSetupApp.Data;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? NoOfPages { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedOn { get; set; }

    public int LanguageId { get; set; }

    public int? AuthorId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<BookPrice> BookPrices { get; set; } = new List<BookPrice>();

    public virtual Language Language { get; set; } = null!;
}
