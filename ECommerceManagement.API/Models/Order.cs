using System;
using System.Collections.Generic;

namespace ECommerceManagement.API.Models;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public Guid AddressId { get; set; }

    public Guid CardId { get; set; }

    public int ShopId { get; set; }

    public float Price { get; set; }

    public DateTime OrderDate { get; set; }

    public bool Status { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Shop Shop { get; set; } = null!;

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
}
