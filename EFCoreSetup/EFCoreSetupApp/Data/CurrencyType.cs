using System;
using System.Collections.Generic;

namespace EFCoreSetupApp.Data;

public partial class CurrencyType
{
    public int Id { get; set; }

    public string Currency { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<BookPrice> BookPrices { get; set; } = new List<BookPrice>();
}
