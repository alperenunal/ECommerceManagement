using System;
using System.Collections.Generic;
using NpgsqlTypes;

namespace ECommerceManagement.API.Models;

public partial class Address
{
    public Guid Id { get; set; }

    public Guid? CustomerId { get; set; }

    public string Address1 { get; set; } = null!;

    public int Zipcode { get; set; }

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public NpgsqlPoint Coordinates { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Store? Store { get; set; }
}
