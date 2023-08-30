using System;
using System.Collections.Generic;

namespace ECommerceManagement.API.Models;

public partial class Request
{
    public Guid Id { get; set; }

    public Guid ManagerId { get; set; }

    public Guid ProductId { get; set; }

    public int ShopId { get; set; }

    public int Amount { get; set; }

    public bool? Status { get; set; }

    public virtual Manager Manager { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Shop Shop { get; set; } = null!;
}
