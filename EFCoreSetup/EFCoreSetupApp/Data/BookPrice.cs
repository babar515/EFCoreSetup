using System;
using System.Collections.Generic;

namespace EFCoreSetupApp.Data;

public partial class BookPrice
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public decimal Amount { get; set; }

    public int CurrencyId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual CurrencyType Currency { get; set; } = null!;
}
