using System;
using System.Collections.Generic;

namespace ECommerceManagement.API.Models;

public partial class Stock
{
    public Guid Id { get; set; }

    public int? StoreId { get; set; }

    public Guid ProductId { get; set; }

    public int Amount { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Store? Store { get; set; }
}
