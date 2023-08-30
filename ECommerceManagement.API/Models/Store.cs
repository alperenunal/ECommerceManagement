using System;
using System.Collections.Generic;

namespace ECommerceManagement.API.Models;

public partial class Store
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid Address { get; set; }

    public bool StoreType { get; set; }

    public virtual Address AddressNavigation { get; set; } = null!;

    public virtual Shop? Shop { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
