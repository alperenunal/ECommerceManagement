using System;
using System.Collections.Generic;

namespace ECommerceManagement.API.Models;

public partial class Manager
{
    public Guid Id { get; set; }

    public int ShopId { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();

    public virtual Shop Shop { get; set; } = null!;
}
